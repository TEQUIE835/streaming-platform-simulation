namespace _1.Application.DTOs.ContentDtos;

public class SeriesDetailDto : ContentDetailDto
{ 
    public IReadOnlyCollection<SeasonDto> Seasons { get; set; } = [];   
}