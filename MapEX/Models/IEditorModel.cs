using System;
namespace MapEX.Models
{
    public interface IEditorModel
    {
        string Name { get; }
        void SetName(string name);
        void ResetName();
    }
}
