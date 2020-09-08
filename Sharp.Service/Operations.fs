module internal Operations

    open System

    let handleResultUnit result =
        match result with
        |Ok _ -> ()
        |Error error -> sprintf "%A" error |> Console.WriteLine

    let handleResultUnitAsync func =
        async {
            let! result = func
            handleResultUnit result }
        |> Async.Start

    let handleResultList result =
        match result with
        |Ok list -> list |> List.toArray 
        |Error error -> sprintf "%A" error |> Console.WriteLine; Array.empty

    let handleResultListAsync func =
        async {
            let! result = func
            return handleResultList result }
        |> Async.StartAsTask
