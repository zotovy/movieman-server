namespace Domain {
    public sealed class Ref<T> {
        public long Id { get; set; } 
        public T? Model { get; set; }

        public Ref(long id, T? model) {
            Id = id;
            if (model != null) Model = model;
        }

        public Ref(long id) {
            Id = id;
        }
        
        public bool IsPopulated() => Model != null;
    }
}