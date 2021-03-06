namespace Bolero

open System
#if !IS_DESIGNTIME
open Microsoft.AspNetCore.Blazor.Components
#endif

/// HTML attribute or Blazor component parameter.
type Attr =
    | Attr of string * obj
    | Attrs of list<Attr>

type BlazorEventHandler<'T> = delegate of 'T -> unit

/// HTML fragment.
type Node =
    /// An empty HTML fragment.
    | Empty
    /// A concatenation of several HTML fragments.
    | Concat of list<Node>
    /// A single HTML element.
    | Elt of name: string * attrs: list<Attr> * children: list<Node>
    /// A single HTML text node.
    | Text of text: string
    /// A raw HTML fragment.
    | RawHtml of html: string
    /// A single Blazor component.
    | Component of Type * attrs: list<Attr> * children: list<Node>
    /// A conditional "if" component.
    | Cond of bool * Node
    /// A conditional "match" component.
    | Match of unionType: Type * value: obj * node: Node
    /// A list of similarly structured fragments.
    | ForEach of list<Node>

// The type provider includes this file.
// TPs fail if the TPDTC references an external type in a signature,
// so the following needs to be excluded from the TP.
// See https://github.com/fsprojects/FSharp.TypeProviders.SDK/issues/274
#if !IS_DESIGNTIME
    static member BlazorComponent<'T when 'T :> IComponent>(attrs, children) =
        Node.Component(typeof<'T>, attrs, children)
#endif
