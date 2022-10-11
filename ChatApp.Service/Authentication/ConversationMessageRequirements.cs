using ChatApp.DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ChatApp.Service.Authentication
{
    public class ConversationMessageRequirements : IAuthorizationRequirement
    {

    }

    public class ConversationMessageRequirementsHandler : AuthorizationHandler<ConversationMessageRequirements>
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ConversationMessageRequirementsHandler(AppDbContext dbContext, 
            IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, 
            ConversationMessageRequirements requirement)
        {
            var username = context.User.FindFirstValue(ClaimTypes.Name);

            if (username == null) return Task.CompletedTask;
            
            //TODO: not sure if i want to send a message with a route value?
            //should maybe be with a param, check this when working on client, cuz this works
            //on the backend
            // Maybe split into another Authorization policy, one for getting conversation,
            //one for sending message to conversation
            //one for editing/deleting a message in a conversation (only user who sent message)
            int conversationId = int.Parse(_httpContextAccessor.HttpContext.Request
                .RouteValues.SingleOrDefault(x => x.Key == "conversationId").Value?.ToString());

            var conversation = _dbContext.Conversations
                .Include(x => x.CreatedByAppUser)
                .Include(x => x.Recipient)
                .FirstOrDefault(x => x.Id == conversationId);

            if (conversation == null) return Task.CompletedTask;

            if(conversation.CreatedByAppUser.UserName == username || 
                conversation.Recipient.UserName == username)
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
