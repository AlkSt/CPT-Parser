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

        #region Properties

        public string Name { get; private set; }
        public List<TreeViewModel> Children { get; private set; }
        public bool IsInitiallySelected { get; private set; }

        bool? _isChecked = false;
        TreeViewModel _parent;

        #region IsChecked

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

        #endregion

        #endregion

        void Initialize()
        {
            foreach (TreeViewModel child in Children)
            {
                child._parent = this;
                child.Initialize();
            }
        }

        public static List<TreeViewModel> SetTree(string topLevelName, Data elementsDataSet)
        {
            List<TreeViewModel> treeView = new List<TreeViewModel>();
            TreeViewModel rootItem = new TreeViewModel(topLevelName);

            treeView.Add(rootItem);

            TreeViewModel parcelItem = new TreeViewModel("Parcel");
            TreeViewModel objectRealtyItem = new TreeViewModel("ObjectRealty");
            TreeViewModel spatialDataItem = new TreeViewModel("SpatialData");
            TreeViewModel boundItem = new TreeViewModel("Bound");
            TreeViewModel zoneItem = new TreeViewModel("Zone");

            elementsDataSet.UploadData();
            
            rootItem.Children.Add(parcelItem);
            InitItems(elementsDataSet.getParcel(), parcelItem);

            rootItem.Children.Add(objectRealtyItem);
            InitItems(elementsDataSet.getObjectRealty(), objectRealtyItem);

            rootItem.Children.Add(spatialDataItem);
            InitItems(elementsDataSet.getSpatial(), spatialDataItem);

            rootItem.Children.Add(boundItem);
            InitItems(elementsDataSet.getBound(), boundItem);

            rootItem.Children.Add(zoneItem);
            InitItems(elementsDataSet.getZone(), zoneItem);

            rootItem.Initialize();

            return treeView;
        }

        public static void InitItems(Dictionary< string, CadastralObject> elements, TreeViewModel viewItem)
        {
            foreach (var elemId in elements.Keys)
                viewItem.Children.Add(new TreeViewModel(elemId));
        }
        public static List<string> GetSelectedChildElements(TreeViewModel TreeViewRoot)
        {
            List<string> selected = new List<string>();
            foreach (var group in TreeViewRoot.Children)
            {
                foreach (var element in group.Children)
                {
                    if (element.IsChecked == true) selected.Add(element.Name);
                }
            }

            return selected;
        }

        #region INotifyPropertyChanged Members

        void NotifyPropertyChanged(string info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
