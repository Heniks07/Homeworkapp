namespace Homeworkapp
{
    public class GetHomeworks
    {
        List<HomeworkItems> items = new List<HomeworkItems>();


        public void add(string Name, string Subject)
        {
            items.Add(new HomeworkItems { Name = Name, Subject = Subject});
        }

        public List<HomeworkItems> GethomeworkItems => items;

        public void delete(HomeworkItems homework)
        {
            items.Remove(homework);
        }
    }
}
