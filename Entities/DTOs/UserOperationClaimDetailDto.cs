using Core.Entities;

namespace Entities.DTOs
{
    public class UserOperationClaimDetailDto : IDto
    {
        public int UserOperationClaimId { get; set; }
        public int UserId { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
    }
}
