namespace ViewModel


module Program =

    type State = { Field1: string; Field2: string }

    type Msg =
        | Event1
        | Event2

    let init () =
        { Field1 = "Initial Value"
          Field2 = "" }

    let update msg s =
        match msg with
        | Event1 -> { s with Field1 = "NewValue1" }
        | Event2 -> { s with Field2 = "NewValue2" }


// Statically typed ViewModel
open Elmish.WPF
open Program

[<AllowNullLiteral>]
type MainViewModel(args) =
    inherit ViewModelBase<State, Msg>(args)

    member _.FieldValue1 = base.Get () (Binding.OneWayT.id >> Binding.mapModel (fun s -> s.Field1))
    member _.FieldValue2 = base.Get () (Binding.OneWayT.id >> Binding.mapModel (fun s -> s.Field2))
    member _.Event1 = base.Get () (Binding.CmdT.setAlways Event1)
    member _.Event2 = base.Get () (Binding.CmdT.setAlways Event2)


module Main =

    open Serilog
    open Serilog.Extensions.Logging

    let main window =

        let logger =
            LoggerConfiguration()
                .MinimumLevel.Override("Elmish.WPF.Update", Events.LogEventLevel.Verbose)
                .MinimumLevel.Override("Elmish.WPF.Bindings", Events.LogEventLevel.Verbose)
                .WriteTo.Debug()
                .CreateLogger()

        WpfProgram.mkSimpleT init update MainViewModel
        |> WpfProgram.withLogger (new SerilogLoggerFactory(logger))
        |> WpfProgram.startElmishLoop window