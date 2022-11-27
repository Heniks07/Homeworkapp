namespace Homeworkapp
{
    public class GetHomeworks
    {
        List<HomeworkItems> items = new List<HomeworkItems>();

        public void add(string Name, string Subject, string Description)
        {
            items.Add(new HomeworkItems { Name = Name, Subject = Subject, Description = Description });
        }

        public List<HomeworkItems> GethomeworkItems => items;

        public void delete(HomeworkItems homework)
        {
            items.Remove(homework);
        }
    }
}
