using System.ComponentModel.DataAnnotations;


namespace Quiz.Contracts.Entities
{
    public class Q_SAVE
    {
        [Key]
        public int ID { get; set; }

        public int USER_ID { get; set; }

        public int QUESTION_ID { get; set; }

        public int CHOICE_ID { get; set; }
    }
}
