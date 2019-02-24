using System;
using System.Text;

namespace LinkList
{
    public class LinkList<T> : ICloneable
    {
        private LinkListNode<T> head;

        public LinkList()
        {
            head = null;
        }

        private LinkList(T headValue)
        {
            head = new LinkListNode<T>
            {
                Value = headValue
            };
        }

        private LinkList(LinkListNode<T> head)
        {
            this.head = head;
        }

        public T Head
        {
            get
            {
                if (head == null)
                {
                    throw new NotSupportedException("Head of an empty List");
                }
                return head.Value;
            }
        }

        public LinkList<T> Tail
        {
            get
            {
                if (head == null)
                {
                    throw new NotSupportedException("Tail of an empty List");
                }
                
                return new LinkList<T>(head.Next);
            }
        }

        public void Add(T value)
        {
            var newEl = new LinkListNode<T>
            {
                Value = value
            };

            var temp = head;

            if (temp == null)
            {
                head = newEl;
                return;
            }

            while (temp.Next != null)
            {
                temp = temp.Next;
            }

            temp.Next = newEl;
        }

        public void Prepend(T value)
        {
            if (head == null)
            {
                head = new LinkListNode<T>
                {
                    Value = value
                };
                return;
            }
            var temp = new LinkListNode<T>
            {
                Value = value,
                Next = head
            };
            head = temp;
        }

        public int Count
        {
            get
            {
                if (head == null)
                {
                    return 0;
                }

                var i = 1;
                var temp = head.Next;
                while (temp != null)
                {
                    i++;
                    temp = temp.Next;
                }

                return i;
            }
        }

        public void DeleteAtIndex(int index)
        {
            if (index > Count - 1)
            {
                throw new ArgumentOutOfRangeException(); 
            }


            if (head.Next == null)
            {
                head = null;
                return;
            }

            if (index == 0)
            {
                head = head.Next;
                return;
            }

            var i = 0;

            var temp = head;

            while (i != index - 1)
            {
                temp = temp.Next;
                i++;
            }

            temp.Next = temp.Next.Next;
        }

        public LinkList<T> Reverse()
        {
            if (head == null)
            {
                return this;
            }

            var (head1, _) = Clone() as LinkList<T> ?? new LinkList<T>();

            var acc = new LinkList<T>(head1.Value);

            var temp = head1.Next;

            while (temp != null)
            {
                acc.Prepend(temp.Value);
                temp = temp.Next;
            }

            return acc;
        }

        public bool IsEmpty()
        {
            return head == null;
        }

        public override string ToString()
        {
            if (head == null)
            {
                return string.Empty;
            }
            
            var sb = new StringBuilder(head.Value.ToString());
            var temp = head.Next;
            
            while (temp != null)
            {
                sb.Append($", {temp.Value}");
                temp = temp.Next;
            }

            return sb.ToString();
        }

        public object Clone()
        {
            if (head == null)
            {
                return new LinkList<T>();
            }

            var temp = head.Next;

            if (temp == null)
            {
                return new LinkList<T>(head.Value);
            }
            
            var buff = new LinkList<T>(head.Value);

            while (temp != null)
            {
                buff.Add(temp.Value);
                temp = temp.Next;
            }

            return buff;
        }

        private void Deconstruct(out LinkListNode<T> head, out LinkList<T> tail)
        {
            head = this.head;
            tail = Tail;
        }
    }
}