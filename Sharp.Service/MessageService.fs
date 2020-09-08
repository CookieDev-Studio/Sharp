namespace Sharp.Service

open Sharp.Data
open System

module MessageService =

    let getMessagesAsync guildId = 
        MessageData.getMessagesAsync guildId |> Operations.handleResultListAsync

    let addMessage guildId channelId userId date attachments message =
        let formatMessage attachments =
                sprintf "%s\n\n%s" (attachments |> String.concat "\n") message

        let formatedMessage =
            match Option.ofObj attachments with
            |Some attachments -> formatMessage attachments
            |None -> message

        MessageData.addMessageAsync guildId channelId userId date formatedMessage 
        |> Operations.handleResultUnitAsync
       