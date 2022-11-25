using GasStation.GraphicEngine.Common;
using GasStation.ConstructorEngine.Life;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace GasStation.ConstructorEngine
{
    class ConstructorArea : DragDropArea<LifeSquare, LifeAppliance>
    {
        private ApplianceType _currentApplicane;
        private bool _showedAvailableZone;
        private Side _roadSide;
        readonly EditorProvider _editorProvider;

        public ConstructorArea(Panel panel, Side roadSide, EditorProvider editorProvider, int widthLength,int heightLength) : base(panel, widthLength, heightLength)
        {
            _editorProvider = editorProvider;
            _roadSide = roadSide;
            InitSubscribers();

            InitArea(SquareSize, widthLength, heightLength, roadSide);
        }


        public ConstructorArea(Panel panel, TopologyTransfer topology, EditorProvider editorProvider) : base(panel, topology.WidthLength, topology.HeightLength)
        {
            _editorProvider = editorProvider;
            InitSubscribers();

            InitArea(SquareSize, topology);
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


        public TopologyTransfer GetTransfer()
        {
            return new TopologyTransfer()
            {
                HeightLength = Heightength,
                WidthLength = WidthLength,
                RowSide = _roadSide,
                Squares = Squares.Select(s => s.GetTransferSquare())
            };
        }

        private bool IsAvailableSquare(ApplianceType appliance, LifeSquare square)
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
                    return (square.Surface.Type == SurfaceType.GasStation || square.Surface.Type == SurfaceType.Service) 
                        && SquareHelper.GetArroundSquares(Squares,square, Heightength, WidthLength).Any(s => s?.Surface.Type == SurfaceType.Road);

                default:
                    throw new Exception();
            }

        }
        private void InitArea(Size size, int widthLength, int heightLength, Side roadSide)
        {
            int id = 0;
            for (int i = 0; i < widthLength; i++)
            {
                for (int j = 0; j < heightLength; j++)
                {
                    var square = new LifeSquare(id, new Point(i * size.Width, j * size.Height), size, _editorProvider.Surfaces[SurfaceType.GasStation]);
                    AddSquare(id, square);
                    id++;
                }
            }

            var length = 0;
            switch (roadSide)
            {
                case Side.Top:
                case Side.Bottom:
                    length = heightLength;
                    break;

                case Side.Left:
                case Side.Right:
                    length = widthLength;
                    break;

                default:
                    break;
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
                        case Side.Top:
                        case Side.Bottom:
                            square = GetSquare(length * (widthLength - j) - i - 1);
                            break;

                        case Side.Left:
                        case Side.Right:
                            square = GetSquare(i * heightLength + j);
                            break;

                        default:
                            break;
                    }

                    square.Surface = _editorProvider.Surfaces[SurfaceType.Service];
                }
            }

            switch (roadSide)
            {
                case Side.Top:
                case Side.Bottom:
                    length = widthLength;
                    break;

                case Side.Left:
                case Side.Right:
                    length = heightLength;
                    break;

                default:
                    break;
            }

            // Строим дороги
            for (int i = 0; i < length; i++)
            {
                LifeSquare square = null;
                switch (roadSide)
                {
                    case Side.Top:
                        square = GetSquare(i * heightLength);
                        break;

                    case Side.Right:
                        square = GetSquare(widthLength * length - i - 1);
                        break;

                    case Side.Bottom:
                        square = GetSquare(heightLength * i + heightLength - 1);
                        break;

                    case Side.Left:
                        square = GetSquare(i);
                        break;
                }

                square.Surface = _editorProvider.Surfaces[SurfaceType.Road];
            }
        }

        private void InitArea(Size size, TopologyTransfer topology) 
        {
            int id = 0;
            for (int i = 0; i < topology.WidthLength; i++)
            {
                for (int j = 0; j < topology.HeightLength; j++)
                {
                    var square = new LifeSquare(id, new Point(i * size.Width, j * size.Height), size, _editorProvider.Surfaces[SurfaceType.GasStation]);
                    AddSquare(id, square);
                    id++;
                }
            }


            foreach (var square in topology.Squares) 
            {
                var currentSquare = GetSquare(square.Id);
                currentSquare.Surface = _editorProvider.Surfaces[square.Surface.Type];

                if(square.LifeAppliance != null)
                {
                    currentSquare.LifeAppliance = _editorProvider.Appliance[square.LifeAppliance.Appliance];
                }             
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
            if (IsAvailableSquare(e.Data.DragDropComponent.Appliance.Type, e.Square))
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
            if (IsAvailableSquare(_currentApplicane, square))
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
            var arround = SquareHelper.GetArroundSquares(Squares,e ,Heightength, WidthLength);
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


        public override void Dispose()
        {
            SuccessDragDropSquare -= SuccessDropSquare;
            DragOverSquare -= OverSquare;
            DragLeaveSquare -= LeaveSquare;
            MouseLeftDownSquare -= LeftDownMouse;
            MouseRightDownSquare -= RightDownMouse;
            MouseMiddleDownSquare -= ConstructorArea_MouseMiddleDownSquare;
            EndDragDrop -= EndDrop;
            DragEnterSquare -= EnterSquare;
            base.Dispose(); 
        }


        private void InitSubscribers()
        {
            SuccessDragDropSquare += SuccessDropSquare;
            DragOverSquare += OverSquare;
            DragLeaveSquare += LeaveSquare;
            MouseLeftDownSquare += LeftDownMouse;
            MouseRightDownSquare += RightDownMouse;
            MouseMiddleDownSquare += ConstructorArea_MouseMiddleDownSquare;
            EndDragDrop += EndDrop;
            DragEnterSquare += EnterSquare;
        }
    }
}
