module internal Operations

    open System

    let handleResultUnit result =
        match result with
        |Ok _ -> ()
        |Error error -> sprintf "%A" error |> Console.WriteLine

    let handleResultList result =
        match result with
        |Ok list -> list |> List.toArray 
        |Error error -> sprintf "%A" error |> Console.WriteLine; Array.empty