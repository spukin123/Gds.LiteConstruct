using System;
using System.Collections.Generic;
using System.Text;

namespace Ism.WebCms.Client.Interface.PropertyDialog
{
    public delegate void ItemGroupChangedEventHandler(Guid itemId, Guid groupId);
    public delegate ILanguagesRelatedItem SelectItemEventHandler();

    public interface ILanguagesUserControl
    {
        event ItemGroupChangedEventHandler ItemGroupChanged;
        event SelectItemEventHandler SelectItem;
        void Init();
        Guid LanguageId{ set; get; }
        ILanguagesRelatedItem CurrentItem { set; get; }
        IList<ILanguagesRelatedItem> RelatedItems { set; get; }
        bool IsNew { set; get; }
        ILanguagesRelatedItem OnSelectItem();
        void OnItemGroupChanged( Guid itemId, Guid groupId);
        void RefreshRelatedItems();
        ILanguagesRelatedItem GetSelectedItem();
        void RemoveSelectedItem();
        string ItemTypeName { get; set; }
        void SetError(string error);
        void ClearError();
    }
}