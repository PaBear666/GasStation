﻿using GasStation.GraphicEngine.Common;
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
        
        public ConstructorArea(Panel panel, EditorProvider surfaceProvider, int size, int length) : base(panel, new Size(size, size), length)
        {
            _surfaceProvider = surfaceProvider;

            InitArea(size, length);
        }      

        public void EndDrop(object sender, EventArgs e)
        {
            ShowNormalZone();
        }

        public void ShowAvailableZone(object sender, DragAndDropData<Appliance> data)
        {
            ShowAvailableZone(data.DragDropComponent.Type);
        }
        public SurfaceType GetAvailableSurface(ApplianceType appliance)
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
        public void ShowNormalZone()
        {
            _showedAvailableZone = false;
            ForSquares((square) =>
            {
                square.ResetDesign();
            });       
        }


        private void InitArea(int size, int length)
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

            SuccessDragDropSquare += SuccessDropSquare;
            DragOverSquare += OverSquare;
            DragLeaveSquare += LeaveSquare;
            MouseDownSquare += DownMouse;
            EndDragDrop += EndDrop;

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

            ForSquares((square) =>
            {
                SetAvailableDesign(square);
            });
        }
        private void SetAvailableDesign(LifeSquare square)
        {
            if (square.Surface.Type == GetAvailableSurface(_currentApplicane))
            {
                square.BaseViewComponent = new ViewComponent(Color.FromArgb(100, Color.Green), square?.Appliance?.ViewComponent?.Image);
            }
            else
            {
                square.BaseViewComponent = new ViewComponent(Color.Red);
            }

        }
    }
}
