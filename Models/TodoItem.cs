namespace TodoApi.Models
{
    public class TodoItem {
        public long id { get; set; }
        public string name { get; set; }
        public bool isComplete { get; set; }

        public TodoItem(string name, bool isComplete) {
            this.name = name;
            this.isComplete = isComplete;
        }
    }
}