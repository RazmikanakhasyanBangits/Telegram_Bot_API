using Grpc.Core;
using System.Threading.Tasks;
using UserActionsProto;

namespace API.GrpcServer
{
    public class UserActionsServer : UserActions.UserActionsBase
    {
        public override Task<BlockUserGrpcResponseModel> BlockUser(BlockUserGrpcRequestModel request, ServerCallContext context)
        {
            return base.BlockUser(request, context);
        }

        public override Task<UnblockUserGrpcResponse> UnblockUser(UnblockUserGrpcRequest request, ServerCallContext context)
        {
            return base.UnblockUser(request, context);
        }
    }
}
