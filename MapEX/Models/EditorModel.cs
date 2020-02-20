using System;
namespace MapEX.Models
{
    public class EditorModel : IEditorModel
    {
        public string Name { get; private set; }

        public void ResetName()
        {
            Name = "";
        }

        public void SetName(string name)
        {
            Name = name;
        }
    }
}
