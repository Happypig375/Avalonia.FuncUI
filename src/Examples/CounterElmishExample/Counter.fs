﻿namespace CounterElmishSample

open Avalonia.Controls
open Avalonia.Media
open Avalonia.FuncUI.Types
open Avalonia.FuncUI

type CustomControl() =
    inherit Control()

    member val Text: string = "" with get, set

[<AutoOpen>]
module ViewExt =
    type Views with
        static member customControl (attrs: TypedAttr<CustomControl> list): View =
            Views.create<CustomControl>(attrs)

module Counter =

    type CounterState = {
        count : int
    }

    let init = {
        count = 0
    }

    type Msg =
    | Increment
    | Decrement

    let update (msg: Msg) (state: CounterState) : CounterState =
        match msg with
        | Increment -> { state with count =  state.count + 1 }
        | Decrement -> { state with count =  state.count - 1 }
    
    let view (state: CounterState) (dispatch): View =
        Views.dockpanel [
            Attrs.children [
                Views.button [
                    Attrs.click (fun sender args -> dispatch Increment)
                    Attrs.content "click to increment"
                ]
                Views.button [
                    Attrs.click (fun sender args -> dispatch Decrement)
                    Attrs.content "click to decrement"
                ]
                Views.textBlock [
                    Attrs.dockPanel_dock Dock.Top
                    Attrs.text (sprintf "the count is %i" state.count)
                ]
            ]
        ]       