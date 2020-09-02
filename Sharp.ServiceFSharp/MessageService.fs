namespace Sharp.FSharp.Service

open System

module MessageService =
    open Sharp.FSharp.Data

    let getMessages = MessageData.getMessages
    let addMessageWithAttachments guildId channelId userId date attachments message =  
        let formatedMessage =
            sprintf "%s\n\n%s"
                (attachments
                |> String.concat("\n"))
                message

        formatedMessage
        |> MessageData.addMessage guildId channelId userId date

    let addMessage guildId channelId userId date message =
        message
        |> MessageData.addMessage guildId channelId userId date
   