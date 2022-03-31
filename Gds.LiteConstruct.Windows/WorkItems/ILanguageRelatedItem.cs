using System;
using System.Collections.Generic;
using System.Text;

namespace Ism.WebCms.Client.Interface.PropertyDialog
{
    public interface ILanguagesRelatedItem
    {
        Guid Id{get;}
        string Name { get;}
        Guid LanguageId { get; set; }
        string LanguageName { get; }
        Guid GroupId{get;}
    }
}
