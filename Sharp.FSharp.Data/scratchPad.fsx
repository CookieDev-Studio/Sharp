
let attachments = 
    [ "a" 
      "b"
      "c" ]

let message = "mesage aadadadad adadada adadad"

let formatedMessage =
    sprintf "%s\n\n%s"
        (attachments
        |> String.concat("\n"))
        message
    

formatedMessage

