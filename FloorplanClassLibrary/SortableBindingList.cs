using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace FloorplanClassLibrary
{


    public class SortableBindingList<T> : BindingList<T>
    {
        private bool isSorted;
        private ListSortDirection sortDirection;
        private PropertyDescriptor sortProperty;

        public SortableBindingList() : base() { }

        public SortableBindingList(IList<T> list) : base(list) { }

        protected override bool SupportsSortingCore => true;
        protected override bool IsSortedCore => isSorted;

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            var items = this.Items as List<T>;

            if (items != null)
            {
                // Toggle sorting direction if the same property is sorted again
                if (sortProperty == prop)
                {
                    direction = sortDirection == ListSortDirection.Ascending
                        ? ListSortDirection.Descending
                        : ListSortDirection.Ascending;
                }

                var property = typeof(T).GetProperty(prop.Name);
                items.Sort((x, y) =>
                {
                    var xValue = property.GetValue(x);
                    var yValue = property.GetValue(y);
                    return direction == ListSortDirection.Ascending
                        ? Comparer.Default.Compare(xValue, yValue)
                        : Comparer.Default.Compare(yValue, xValue);
                });

                isSorted = true;
                sortDirection = direction;
                sortProperty = prop;
            }
            else
            {
                isSorted = false;
            }
            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        protected override void RemoveSortCore()
        {
            isSorted = false;
            sortDirection = ListSortDirection.Ascending;
            sortProperty = null;
        }
    }

}
