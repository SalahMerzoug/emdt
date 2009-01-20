// ItemPropertyChangedEventArgs.cs by Charles Petzold, June 2008
//      http://msdn.microsoft.com/fr-fr/magazine/cc794276.aspx
using System.ComponentModel;

public class ItemPropertyChangedEventArgs : PropertyChangedEventArgs
{
    object item;

    public ItemPropertyChangedEventArgs(object item,
                                        string propertyName)
        : base(propertyName)
    {
        this.item = item;
    }

    public object Item
    {
        get { return item; }
    }
}

public delegate void ItemPropertyChangedEventHandler(object sender,
                                    ItemPropertyChangedEventArgs args);

