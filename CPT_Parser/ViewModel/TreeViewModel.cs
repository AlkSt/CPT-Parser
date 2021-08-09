using CPT_Parser.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPT_Parser
{
    class TreeViewModel : INotifyPropertyChanged
    {
        TreeViewModel(string name)
        {
            Name = name;
            Children = new List<TreeViewModel>();
        }

        public string Name { get; private set; }
        public List<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }

        bool? _isChecked = false;
        TreeViewModel _parent;



        public bool? IsChecked
        {
            get { return _isChecked; }
            set { SetIsChecked(value, true, true); }
        }

        void SetIsChecked(bool? value, bool updateChildren, bool updateParent)
        {
            if (value == _isChecked) return;

            _isChecked = value;

            if (updateChildren && _isChecked.HasValue) Children.ForEach(c => c.SetIsChecked(_isChecked, true, false));

            if (updateParent && _parent != null) _parent.VerifyCheckedState();

            NotifyPropertyChanged("IsChecked");
        }

        void VerifyCheckedState()
        {
            bool? state = null;

            for (int i = 0; i < Children.Count; ++i)
            {
                bool? current = Children[i].IsChecked;
                if (i == 0)
                {
                    state = current;
                }
                else if (state != current)
                {
                    state = null;
                    break;
                }
            }

            SetIsChecked(state, false, true);
        }


        void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public static List<TreeViewModel> SetTree(string topLevelName, DataCadastralSet elementsDataSet)
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();
            TreeViewModel rootItem = new TreeViewModel(topLevelName);

            treeView.Add(rootItem);

            TreeViewModel parcelItem = new TreeViewModel("Parcel/Земельные участки");
            TreeViewModel objectRealtyItem = new TreeViewModel("ObjectRealty/Объекты недвижимости");
            TreeViewModel spatialDataItem = new TreeViewModel("SpatialData/Пространственные данные");
            TreeViewModel boundItem = new TreeViewModel("Bound/Границы");
            TreeViewModel zoneItem = new TreeViewModel("Zone/Зоны");
            
            rootItem.Children.Add(parcelItem);
            InitItems(elementsDataSet.ParcelElenents, parcelItem);

            rootItem.Children.Add(objectRealtyItem);
            InitItems(elementsDataSet.ObjectRealtyElenents, objectRealtyItem);

            rootItem.Children.Add(spatialDataItem);
            InitItems(elementsDataSet.SpatialElenents, spatialDataItem);

            rootItem.Children.Add(boundItem);
            InitItems(elementsDataSet.BoundElenents, boundItem);

            rootItem.Children.Add(zoneItem);
            InitItems(elementsDataSet.ZoneElenents, zoneItem);

            rootItem.Initialize();

            return treeView;
        }

        public static void InitItems(Dictionary< string, CadastralObject> elements, TreeViewModel viewItem)
        {
            foreach (var elemId in elements.Keys)
                viewItem.Children.Add(new TreeViewModel(elemId));
        }
        public static List<List<string>> GetSelectedChildElements(TreeViewModel TreeViewRoot)
        {
            List<List<string>> selectedGroup = new List<List<string>>();
            foreach (var group in TreeViewRoot.Children)
            {
                List<string> selected = new List<string>();
                foreach (var element in group.Children)
                    if (element.IsChecked == true)
                        selected.Add(element.Name);

                if (selected.Count > 0)
                    selectedGroup.Add(selected);
            }

            return selectedGroup;
        }


        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


    }
}
