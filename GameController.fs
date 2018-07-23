module GameController

open GameCore
open Model
open Microsoft.Xna.Framework.Input

let handlePlayingState runState worldState =
    worldState
    |> KnightController.processKnight runState
    |> Playing |> Some

let advanceGame (runState : RunState) =
    function
    | None -> MapLoader.getLevel 1 |> getLevelModel |> Some 
    | _ when runState.WasJustPressed Keys.Escape -> None
    | Some model -> 
        match model with
        | Playing worldState when 
                worldState.knight.state = Dead && 
                runState.WasJustPressed Keys.R -> 
            MapLoader.getLevel 1 |> getLevelModel |> Some
        | Playing worldState -> 
            handlePlayingState runState worldState
        | _ -> Some model