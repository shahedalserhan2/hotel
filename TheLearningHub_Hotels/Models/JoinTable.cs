namespace TheLearningHub_Hotels.Models
{
    public class JoinTable
    {
        public CHotel hotel { get; set; }
        public CTestimonial testimonial { get; set; }
        public CUserlogin userlogin { get; set; }
        public CRoom room { get; set; }
        public CService service { get; set; }
        public CUser user { get; set; }
        public CReservation reservation { get; set; }
      
      

    }
}
