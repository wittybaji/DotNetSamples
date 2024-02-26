namespace DataGridSample
{
    public class SystemParam : ObservableObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SystemParamType ParamType { get; set; }

        private string paramValue;
        public string ParamValue
        {
            get => paramValue;
            set 
            { 
                paramValue = value;
                OnPropertyChanged(nameof(ParamValue));
            }
        }
    }

    public enum SystemParamType : byte
    {
        Bool = 1,
        Int = 2,
        String = 3,
    }
}
