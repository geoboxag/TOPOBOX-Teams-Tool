using System.Collections.Generic;
using System.Linq;
using TOPOBOX.OSC.TeamsTool.Common.DAL;
using Microsoft.Graph;

namespace TOPOBOX.OSC.TeamsTool.Common.Mapper
{
    public class GraphItemMapping
    {

    //#region Planner-Methods

    //public PlannerTask MapGbxTaskToPlannerTask(GbxTask gbxTask, PlannerPreviewType previewType)
    //{
    //    PlannerChecklistItems plannerChecklistItems =
    //        MapListOfGbxCheckListItemToPlannerChecklistItems(gbxTask.Checklist);

    //    var plannerTaskDetails = GetPlannerTaskDetails(gbxTask.TaskDescription, previewType, plannerChecklistItems);

    //    return new PlannerTask()
    //    {
    //        Title = gbxTask.Title,
    //        Details = plannerTaskDetails
    //    };
    //}



    //private PlannerChecklistItems MapListOfGbxCheckListItemToPlannerChecklistItems(
    //    List<GbxChecklistItem> gbxCheckListItems)
    //{
    //    PlannerChecklistItems plannerChecklistItems = new PlannerChecklistItems();
    //    foreach (GbxChecklistItem gbxChecklistItem in gbxCheckListItems)
    //    {
    //        if (!string.IsNullOrEmpty(gbxChecklistItem.Description))
    //        {
    //            plannerChecklistItems.AddChecklistItem(gbxChecklistItem.Description);
    //        }
    //    }

    //    return plannerChecklistItems;
    //}



    //public PlannerTaskDetails GetPlannerTaskDetails(
    //    string description, PlannerPreviewType previewType, PlannerChecklistItems plannerChecklistItems = null)
    //{
    //    if (plannerChecklistItems is null)
    //    {
    //        return new PlannerTaskDetails()
    //        {
    //            Description = description,
    //            PreviewType = previewType,
    //        };
    //    }

    //    return new PlannerTaskDetails()
    //    {
    //        Description = description,
    //        PreviewType = previewType,
    //        Checklist = plannerChecklistItems,
    //    };

    //}



    //public PlannerAssignments GetPlannerAssignments(string userId)
    //{
    //    if (string.IsNullOrEmpty(userId)) return null;

    //    var plannerAssignments = new PlannerAssignments();
    //    plannerAssignments.AddAssignee(userId);
    //    return plannerAssignments;
    //}



    //public GbxTask MapPlannerTaskToGbxTask(PlannerTask plannerTask)
    //{
    //    GbxTask gbxTask = new GbxTask()
    //    {
    //        Title = plannerTask.Title,
    //    };

    //    if (plannerTask.Details != null)
    //    {
    //        if (plannerTask.HasDescription.GetValueOrDefault(false))
    //        {
    //            gbxTask.TaskDescription = plannerTask.Details.Description;
    //        }

    //        if (plannerTask.Details.Checklist != null && plannerTask.Details.Checklist.Any())
    //        {
    //            gbxTask.Checklist = MapPlannerChecklistItemsToListOfGbxCheckListItem(plannerTask.Details.Checklist);
    //        }
    //    }

    //    return gbxTask;
    //}


    //private List<GbxChecklistItem> MapPlannerChecklistItemsToListOfGbxCheckListItem(
    //    PlannerChecklistItems plannerChecklistItems)
    //{
    //    List<GbxChecklistItem> gbxCheckListItems = new List<GbxChecklistItem>();

    //    foreach (var plannerChecklistItem in plannerChecklistItems)
    //    {
    //        GbxChecklistItem gbxChecklistItem = new GbxChecklistItem();

    //        if (!string.IsNullOrEmpty(plannerChecklistItem.Value.Title))
    //        {
    //            gbxChecklistItem.Description = plannerChecklistItem.Value.Title;
    //            gbxChecklistItem.IsChecked = plannerChecklistItem.Value.IsChecked ?? false;
    //            gbxCheckListItems.Add(gbxChecklistItem);
    //        }

    //    }

    //    return gbxCheckListItems;
    //}

    //#endregion


    //#region ChatMessages
    //private GbxChatMessage MapToGbxChatMessage(ChatMessage chatMessage)
    //{
    //    var chatMessageReplies = new List<GbxChatMessage>();
    //    if (chatMessage.Replies != null && chatMessage.Replies.Any())
    //    {
    //        chatMessageReplies = MapToGbxChatMessages(chatMessage.Replies.ToList());
    //    }

    //    return new GbxChatMessage()
    //    {
    //        Id = chatMessage.Id,
    //        ReplyToId = chatMessage.ReplyToId,
    //        MessageType = chatMessage.MessageType.Value,
    //        CreatedDateTime = chatMessage.CreatedDateTime,
    //        LastModifiedDateTime = chatMessage.LastModifiedDateTime,
    //        LastEditedDateTime = chatMessage.LastEditedDateTime,
    //        Subject = chatMessage.Subject,
    //        Summary = chatMessage.Summary,
    //        ChatId = chatMessage.ChatId,
    //        Importance = chatMessage.Importance,
    //        User = MapToGbxUser(chatMessage.From),               
    //        GbxMessageBody = MapToGbxMessageBody(chatMessage.Body),
    //        GbxChannelIdentity = MapToGbxChannelIdentity(chatMessage.ChannelIdentity),
    //        GbxMentions = MapToGbxMentions(chatMessage.Mentions),
    //        GbxAttachments = MapToGbxAttachments(chatMessage.Attachments), 
    //        MessageReplies = chatMessageReplies
    //    };
    //}



    //public List<GbxAttachment> MapToGbxAttachments(IEnumerable<ChatMessageAttachment> attachments)
    //{
    //    List<GbxAttachment> gbxAttachments = new List<GbxAttachment>();
    //    foreach (var attachment in attachments)
    //    {
    //        gbxAttachments.Add(new GbxAttachment()
    //        {
    //            Id = attachment.Id,
    //            Name = attachment.Name,
    //            ContentType = attachment.ContentType,
    //            ContentUrl = attachment.ContentUrl,
    //            Content = attachment.Content
    //        });               
    //    }
    //    return gbxAttachments;
    //}


    //public IEnumerable<ChatMessageAttachment> MapToChatMessageAttachments(List<GbxAttachment> attachments)
    //{
    //    List<ChatMessageAttachment> chatMessageAttachments = new List<ChatMessageAttachment>();
    //    foreach (var attachment in attachments)
    //    {
    //        chatMessageAttachments.Add(new ChatMessageAttachment()
    //        {
    //            Id = attachment.Id,
    //            Name = attachment.Name,
    //            ContentType = attachment.ContentType,
    //            ContentUrl = attachment.ContentUrl,
    //            Content = attachment.Content
    //        });
    //    }
    //    return chatMessageAttachments;
    //}


    //public List<GbxMention> MapToGbxMentions(IEnumerable<ChatMessageMention> mentions)
    //{
    //    List<GbxMention> gbxMentions = new List<GbxMention>();
    //    foreach (var mention in mentions)
    //    {
    //        gbxMentions.Add(new GbxMention()
    //        {
    //            Id = mention.Id,
    //            Text = mention.MentionText,
    //            GbxUser = new GbxUser()
    //            {
    //                Id = mention.Mentioned.User.Id,
    //                Name = mention.Mentioned.User.DisplayName
    //            }

    //        });

    //    }
    //    return gbxMentions;
    //}


    //public IEnumerable<ChatMessageMention> MapToChatMessageMentions(List<GbxMention> mentions)
    //{
    //    List<ChatMessageMention> chatMessageMentions = new List<ChatMessageMention>();
    //    foreach (var mention in mentions)
    //    {
    //        chatMessageMentions.Add(new ChatMessageMention()
    //        {
    //            Id = mention.Id,
    //            MentionText = mention.Text,
    //            Mentioned = MapToIdentitySet(mention.GbxUser)
    //        });
    //    }
    //    return chatMessageMentions;
    //}


    //public IEnumerable<ChatMessage> MapToChatMessages(List<GbxChatMessage> gbxChatMessages)
    //{
    //    List<ChatMessage> chatMessages = new List<ChatMessage>();
    //    foreach (var gbxChatMessage in gbxChatMessages)
    //    {
    //        chatMessages.Add(MapToChatMessage(gbxChatMessage));
    //    }
    //    return chatMessages;
    //}


    //public List<GbxChatMessage> MapToGbxChatMessages(List<ChatMessage> chatMessages)
    //{
    //    List<GbxChatMessage> gbxChatMessages = new List<GbxChatMessage>();
    //    foreach (var chatMessage in chatMessages)
    //    {
    //        gbxChatMessages.Add(MapToGbxChatMessage(chatMessage));
    //    }
    //    return gbxChatMessages;
    //}


    //private ChatMessage MapToChatMessage(GbxChatMessage gbxChatMessage)
    //{
    //    return new ChatMessage()
    //    {
    //        MessageType = gbxChatMessage.MessageType.Value,
    //        CreatedDateTime = gbxChatMessage.CreatedDateTime,
    //        LastModifiedDateTime = gbxChatMessage.LastModifiedDateTime,
    //        LastEditedDateTime = gbxChatMessage.LastEditedDateTime,
    //        Subject = gbxChatMessage.Subject,
    //        Summary = gbxChatMessage.Summary,
    //        ChatId = gbxChatMessage.ChatId,
    //        Importance = gbxChatMessage.Importance,
    //        From = MapToIdentitySet(gbxChatMessage.User),
    //        Body = MapToItemBody(gbxChatMessage.GbxMessageBody),
    //        ChannelIdentity = MapToChannelIdentity(gbxChatMessage.GbxChannelIdentity),
    //        Attachments = MapToChatMessageAttachments(gbxChatMessage.GbxAttachments),
    //        Mentions = MapToChatMessageMentions(gbxChatMessage.GbxMentions),
    //        Replies = MapToChatMessageRepliesCollectionPage(gbxChatMessage.MessageReplies)

    //    };
    //}

    //private IChatMessageRepliesCollectionPage MapToChatMessageRepliesCollectionPage(List<GbxChatMessage> messageReplies)
    //{
    //    var collPages = new ChatMessageRepliesCollectionPage();
    //    foreach (var messageReply in messageReplies)
    //    {
    //        collPages.Add(MapToChatMessage(messageReply));
    //    }

    //    return collPages;
    //}

    //private GbxMessageBody MapToGbxMessageBody(ItemBody itemBody)
    //{
    //    return new GbxMessageBody()
    //    {
    //        ContentType = itemBody.ContentType.Value,
    //        Content = itemBody.Content
    //    };
    //}

    //private ItemBody MapToItemBody(GbxMessageBody gbxMessageBody)
    //{
    //    return new ItemBody()
    //    {
    //        ContentType = gbxMessageBody.ContentType.Value,
    //        Content = gbxMessageBody.Content
    //    };
    //}

    //private ChannelIdentity MapToGbxChannelIdentity(ChannelIdentity channelIdentity)
    //{
    //    return new ChannelIdentity()
    //    {
    //        ChannelId = channelIdentity.ChannelId,
    //        TeamId = channelIdentity.TeamId
    //    };
    //}

    //private ChannelIdentity MapToChannelIdentity(ChannelIdentity gbxChannelIdentity)
    //{
    //    return new ChannelIdentity()
    //    {
    //        ChannelId = gbxChannelIdentity.ChannelId,
    //        TeamId = gbxChannelIdentity.TeamId
    //    };
    //}
    //#endregion


}
}
