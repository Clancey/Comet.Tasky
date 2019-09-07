using Comet;

namespace Tasky
{

    public class TodoItem : BindingObject
    {
        public int ID
        {
            get => this.GetProperty<int>();
            set => this.SetProperty(value);
        }
        public string Name
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }
        public string Notes
        {
            get => this.GetProperty<string>();
            set => this.SetProperty(value);
        }
        public bool Done
        {
            get => this.GetProperty<bool>();
            set => this.SetProperty(value);
        }
    }

}