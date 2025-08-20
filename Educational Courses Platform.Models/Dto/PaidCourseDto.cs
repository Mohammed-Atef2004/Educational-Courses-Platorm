public class PaidCourseDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public List<EpisodeDto>? Episodes { get; set; }  
}

