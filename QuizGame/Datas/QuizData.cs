namespace QuizGame
{
    public class QuizData
    {
        public static int IconsCount {  get; set; }
        public string[] Questions { get; } = {
            "[you] [web] [carry] [enemy] [death]",
            "[you] [star] [shine] [bright]",
            "От [win] [us] отделяет лишь [one] [fight]",
        };

        public string[] Answers { get; } = {
            "Твоя сеть несет врагам гибель",
            "Твоя звезда светит ярко",
            "От победы нас отделяет лишь один бой",
        };

        public string[] SentencesImages { get; } = {
            "you.png",
            "web.png",
            "carry.png",
            "enemy.png",
            "death.png",

            "star.png",
            "shine.png",
            "bright.png",
            "bright.png",
            
            "win.png",
            "one.png",
            "fight.png",
            "us.png",
        };
    }
}
