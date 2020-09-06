
let attachments = 
    [ "a" 
      "b"
      "c" ]

let message = "mesage aadadadad adadada adadad"


let addMessageWithAttachments guildId channelId userId date attachments (message : string) =
    let formatMessage attachments : string =
            sprintf "%s\n\n%s" (attachments |> String.concat "\n") message

    match Option.ofObj attachments with
    |Some attachments -> formatMessage attachments
    |None -> message
    
addMessageWithAttachments 1 1 1 1 null message