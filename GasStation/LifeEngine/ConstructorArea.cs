using GasStation.GraphicEngine.Common;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GasStation.LifeEngine
{
    class ConstructorArea : Area<LifeSquare, LifeAppliance>
    {
        private ApplianceType _currentApplicane;
        private bool _showedAvailableZone;
        readonly EditorProvider _editorProvider;
        
        public ConstructorArea(Panel panel, Side roadSide, EditorProvider editorProvider, int size, int length) : base(panel, new Size(size, size), length)
        {
            _editorProvider = editorProvider;

            InitArea(size, length, roadSide);

            SuccessDragDropSquare += SuccessDropSquare;
            DragOverSquare += OverSquare;
            DragLeaveSquare += LeaveSquare;
            MouseLeftDownSquare += LeftDownMouse;
            MouseRightDownSquare += RightDownMouse;
            MouseMiddleDownSquare += ConstructorArea_MouseMiddleDownSquare;
            EndDragDrop += EndDrop;
            DragEnterSquare += EnterSquare;
        }

        private void ConstructorArea_MouseMiddleDownSquare(object sender, SquareArgs<LifeSquare> e)
        {
            if (e.Square.LifeAppliance != null)
            {
                e.Square.LifeAppliance = null;
            }
        }

        private void RightDownMouse(object sender, SquareArgs<LifeSquare> e)
        {
            if (e.Square.LifeAppliance != null) 
            {
                switch (e.Square.LifeAppliance.Appliance.Side)
                {
                    case Side.Top:
                        e.Square.LifeAppliance = _editorProvider.Appliance[new Appliance(e.Square.LifeAppliance.Appliance.Type, Side.Right)];
                        break;
                    case Side.Right:
                        e.Square.LifeAppliance = _editorProvider.Appliance[new Appliance(e.Square.LifeAppliance.Appliance.Type, Side.Bottom)];
                        break;
                    case Side.Bottom:
                        e.Square.LifeAppliance = _editorProvider.Appliance[new Appliance(e.Square.LifeAppliance.Appliance.Type, Side.Left)];
                        break;
                    case Side.Left:
                        e.Square.LifeAppliance = _editorProvider.Appliance[new Appliance(e.Square.LifeAppliance.Appliance.Type, Side.Top)];
                        break;
                    default:
                        break;
                }
            }

        }

        public void EndDrop(object sender, EventArgs e)
        {
            ShowNormalZone();
        }

        public void ShowAvailableZone(object sender, DragAndDropData<LifeAppliance> data)
        {
            ShowAvailableZone(data.DragDropComponent.Appliance.Type);
        }
      
        public void ShowNormalZone()
        {
            _showedAvailableZone = false;
            ForSquares((square) =>
            {
                square.ResetDesign();
            });       
        }

        private bool isAvailableSquare(ApplianceType appliance, LifeSquare square)
        {
            switch (appliance)
            {
                case ApplianceType.Shop:
                    return square.Surface.Type == SurfaceType.GasStation;

                case ApplianceType.GasStation:
                    return square.Surface.Type == SurfaceType.GasStation;

                case ApplianceType.Tanker:
                    return square.Surface.Type == SurfaceType.Service;

                case ApplianceType.Bridge:
                    return square.Surface.Type == SurfaceType.GasStation 
                        && GetArroundSquares(square).Any(s => s?.Surface.Type == SurfaceType.Road);

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
                    var square = new LifeSquare(id, new Point(i * size, j * size), new Size(size, size), _editorProvider.Surfaces[SurfaceType.GasStation]);
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

                    square.Surface = _editorProvider.Surfaces[SurfaceType.Service];
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

                square.Surface = _editorProvider.Surfaces[SurfaceType.Road];
            }
        }
        private void LeftDownMouse(object sender, SquareArgs<LifeSquare> e)
        {
            if (e.Square.LifeAppliance != null)
            {
                e.Square.Control.DoDragDrop(new DragAndDropData<LifeAppliance>(e.Square.LifeAppliance, () =>
                {
                    e.Square.LifeAppliance = null;
                }), DragDropEffects.All);
            }
        }
        private void LeaveSquare(object sender, SquareArgs<LifeSquare> e)
        {
            if (_showedAvailableZone)
            {
                SetAvailableDesignStatus(e.Square); 
            }
        }


        private void OverSquare(object sender, SquareDragDropArgs<LifeAppliance, LifeSquare> e)
        {
            ShowAvailableZone(e.Data.DragDropComponent.Appliance.Type);      
        }

        private void EnterSquare(object sender, SquareDragDropArgs<LifeAppliance, LifeSquare> e)
        {
            e.Square.SetFrontImage(e.Data.DragDropComponent.ViewComponent.Image);
        }

        private void SuccessDropSquare(object sender, SquareDragDropArgs<LifeAppliance, LifeSquare> e)
        {
            if (isAvailableSquare(e.Data.DragDropComponent.Appliance.Type, e.Square))
            {
                e.Data.FinishDragDrop?.Invoke();
                e.Square.LifeAppliance = e.Data.DragDropComponent;
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
            
            ForSquares((square) => SetAvailableDesignStatus(square));
           
        }
        private void SetAvailableDesignStatus(LifeSquare square)
        {
            if (isAvailableSquare(_currentApplicane, square))
            {
                square.FillColor(Color.Green);
                square.ShowAppliance();
            }
            else
            {
                square.FillColor(Color.Red);
                square.HideAppliance();
            }
        }

        private LifeSquare GetSideSquare(Side side, LifeSquare e)
        {
            var arround = GetArroundSquares(e);
            switch (side)
            {
                case Side.Top:
                    return arround[3];

                case Side.Right:
                    return arround[7];

                case Side.Bottom:
                    return arround[5];
                    
                case Side.Left:
                    return arround[1];
                default:
                    throw new Exception();
            }
        }
    }
}
