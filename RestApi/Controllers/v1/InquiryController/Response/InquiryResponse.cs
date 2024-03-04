using Persistence.Models.Enums;

namespace RestApi.Controllers.v1.InquiryController.Response
{
	public class InquiryResponse
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public InquiryType Type { get; set; }
        public InquiryStatus Status { get; set; }
    }
}
