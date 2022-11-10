using System;

namespace GasStation.GraphicEngine.Common
{
    public class DragAndDropData<T>
    {
        public T DragDropComponent { get; set; }

        public Action FinishDragDrop { get; set; }
        public DragAndDropData(T dragDropComponent, Action finishDragDrop)
        {
            DragDropComponent = dragDropComponent;
            FinishDragDrop = finishDragDrop;
        }
    }
}
