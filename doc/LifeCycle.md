# Life Cycles

- Application Life Cycle
- Request Life Cycle

    
    Pre Application Start
    Application Start
        Request Life Cycle
        ...
        Request Life Cycle
        ...
    Application End


## The Application and Request Life Cycle Events


    MvcApplication
        Application_Start
        Application_BeginRequest
        Application_AuthenticateRequest
        Application_EndRequest
        Application_End


## asp.net Platform

- HttpModule
- HttpHandler


## the asp.net life cycles

    BeginRequest
        ResolveRequestCache
            MapRequestHandler
                AcquireRequestState
                    RequestHandlerExecute [controller & action here!]
                        UpdateRequestCache
                            LogRequest
                                EndRequest  

## the mvc framework

    Controllers
    Action Methods
    Models
    Filters
    Views

## asp.net webforms

HttpHandler
    OnPreInit
    OnInit
    ...
    Render
    Unload

## asp.net mvc

Request -> Routing -> Controller Initialization -> Action Execution

    Url Routing Module
    Mvc RouteHandler
    Mvc HttpHanlder

                ControllerFactory Activator DI
                                                Model Binding
                                                Action Filters(Executing)
                                                Action Execution
                                                Action Filters(Executed)
                                                Action Result


 -> Result Executing  -> View Engine ->Result Executed -> Response