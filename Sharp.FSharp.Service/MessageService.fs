namespace Sharp.FSharp.Service

module MessageService =
    open Sharp.FSharp.Data

    let getMessages guildId = MessageData.getMessages guildId

    let addMessage guildId channelId userId date attachments (message : string) =
        let formatMessage attachments : string =
                sprintf "%s\n\n%s" (attachments |> String.concat "\n") message

        let formatedMessage =
            match Option.ofObj attachments with
            |Some attachments -> formatMessage attachments
            |None -> message

        MessageData.addMessage guildId channelId userId date formatedMessage