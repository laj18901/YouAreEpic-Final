using System.Collections.Generic;

namespace YouAreEpic.Backend.Dtos
{
    public class NonprofitorganisationResponseDto
    {
        public string LogoLink { get; set; }

        public string Name { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        public string WebsiteLink { get; set; }

        public IList<CategoryResponseDto> Categories { get; set; }
    }
}
