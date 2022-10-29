using GasStation.GraphicEngine.Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    class ConstructorArea : Area<LifeSquare, Appliance>
    {
        private ApplianceType _currentApplicane;
        private bool _showedAvailableZone;
        readonly EditorProvider _surfaceProvider;
        
        public ConstructorArea(Panel panel, Side roadSide, EditorProvider surfaceProvider, int size, int length) : base(panel, new Size(size, size), length)
        {
            _surfaceProvider = surfaceProvider;

            InitArea(size, length, roadSide);



            SuccessDragDropSquare += SuccessDropSquare;
            DragOverSquare += OverSquare;
            DragLeaveSquare += LeaveSquare;
            MouseDownSquare += DownMouse;
            EndDragDrop += EndDrop;
        }

        public void EndDrop(object sender, EventArgs e)
        {
            ShowNormalZone();
        }

        public void ShowAvailableZone(object sender, DragAndDropData<Appliance> data)
        {
            ShowAvailableZone(data.DragDropComponent.Type);
        }
      
        public void ShowNormalZone()
        {
            _showedAvailableZone = false;
            ForSquares((square) =>
            {
                square.ResetDesign();
            });       
        }

        private SurfaceType GetAvailableSurface(ApplianceType appliance)
        {
            switch (appliance)
            {
                case ApplianceType.Shop:
                    return SurfaceType.GasStation;
                case ApplianceType.GasStation:
                    return SurfaceType.GasStation;
                case ApplianceType.Tanker:
                    return SurfaceType.Service;
                default:
                    throw new Exception();
            }

        }
        private void InitArea(int size, int length, Side roadSide)
        {
            int id = 0;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    var square = new LifeSquare(id, new Point(i * size, j * size), new Size(size, size), _surfaceProvider.Surfaces[SurfaceType.GasStation]);
                    AddSquare(id, square);
                    id++;
                }
            }


            // Строим сервисную часть АЗС
            int serviceGasStationWidth = 4;
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < serviceGasStationWidth; j++)
                {
                    LifeSquare square = null;

                    switch (roadSide)
                    {
                        case Side.Top | Side.Bottom:
                            square = GetSquare(length * (length - j) - i - 1);
                            break;

                        case Side.Left | Side.Right:
                            square = GetSquare(i * length + j);
                            break;

                        default:
                            break;
                    }

                    square.Surface = _surfaceProvider.Surfaces[SurfaceType.Service];
                }
            }

            // Строим дороги
            for (int i = 0; i < length; i++)
            {
                LifeSquare square = null;
                switch (roadSide)
                {
                    case Side.Top:
                        square = GetSquare(i * length);
                        break;

                    case Side.Right:
                        square = GetSquare(length * length - i - 1);
                        break;

                    case Side.Bottom:
                        square = GetSquare((length - i) * length - 1);
                        break;

                    case Side.Left:
                        square = GetSquare(i);
                        break;
                }

                square.Surface = _surfaceProvider.Surfaces[SurfaceType.Road];
            }
        }
        private void DownMouse(object sender, SquareArgs<LifeSquare> e)
        {
            if (e.Square.Appliance != null)
            {
                e.Square.Control.DoDragDrop(new DragAndDropData<Appliance>(e.Square.Appliance, () =>
                {
                    e.Square.Appliance = null;


                }), DragDropEffects.All);
            }
        }
        private void LeaveSquare(object sender, SquareArgs<LifeSquare> e)
        {
            if (_showedAvailableZone)
            {
                SetAvailableDesign(e.Square);
            }
            else
            {
                e.Square.ResetDesign();
            }

        }
        private void OverSquare(object sender, SquareDragDropArgs<Appliance, LifeSquare> e)
        {
            ShowAvailableZone(e.Data.DragDropComponent.Type);
            e.Square.SetDesign(e.Data.DragDropComponent.ViewComponent);
        }
        private void SuccessDropSquare(object sender, SquareDragDropArgs<Appliance, LifeSquare> e)
        {
            if (GetAvailableSurface(e.Data.DragDropComponent.Type) == e.Square.Surface.Type)
            {
                e.Data.FinishDragDrop?.Invoke();
                e.Square.Appliance = e.Data.DragDropComponent;

            }
            else
            {
                e.Square.ResetDesign();
            }

        }
        private void ShowAvailableZone(ApplianceType appliance)
        {
            if (_showedAvailableZone)
            {
                return;
            }
            else
            {
                _showedAvailableZone = true;
                _currentApplicane = appliance;
            }
            
            ForSquares((square) => SetAvailableDesign(square));
            
        }
        private void SetAvailableDesign(LifeSquare square)
        {
            if (square.Surface.Type == GetAvailableSurface(_currentApplicane))
            {
                square.BaseViewComponent = new ViewComponent(Color.FromArgb(100, Color.Green), square?.Appliance?.ViewComponent?.Image);
            }
            else
            {
                square.BaseViewComponent = new ViewComponent(Color.FromArgb(100, Color.Red));
            }

        }


    }
}
