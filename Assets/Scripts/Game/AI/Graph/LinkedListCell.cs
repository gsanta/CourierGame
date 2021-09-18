

namespace AI
{
    public class LinkedListCell<T>
    {
        private T data;
        private LinkedListCell<T> next;

        public T Data
        {
            get => data;
            set => data = value;
        }

        public LinkedListCell<T> Next
        {
            get => next;
            set => next = value;
        }
    }
}

