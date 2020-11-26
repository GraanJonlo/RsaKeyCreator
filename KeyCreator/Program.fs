module File =
    open System.IO

    let writeAllText filename text =
        File.WriteAllText(filename, text);

module Crypto =
    open System.Security.Cryptography

    type Key =
    | PublicKey of string
    | PrivateKey of string

    let rsaKeysOfSize (size:int) =
        use cryptoServiceProvider = new RSACryptoServiceProvider(size)
        let privateKey = cryptoServiceProvider.ToXmlString(true) |> PrivateKey
        let publicKey = cryptoServiceProvider.ToXmlString(false) |> PublicKey
        [|publicKey; privateKey|]

let writeKey filename =
    function
    | Crypto.PublicKey pk ->
        File.writeAllText (filename + "_pub.xml") pk
    | Crypto.PrivateKey pk ->
        File.writeAllText (filename + ".xml") pk

let writeKeys filename =
    Seq.iter (writeKey filename)

open CommandLine

type Options = {
  [<Option('f', "filename", Required = false, HelpText = "Filename.", Default = "rsa_key")>]
  Filename : string

  [<Option('s', "size", Required = false, HelpText = "Key size in bits.", Default = 2048)>]
  Size: int
}

open System.Collections.Generic

let fail (errs: IEnumerable<Error>) =
    errs
    |> Seq.iter (fun x -> printfn "%A" x)
    1

let run opts =
    Crypto.rsaKeysOfSize opts.Size
    |> writeKeys opts.Filename
    0

[<EntryPoint>]
let main argv =
  let result = CommandLine.Parser.Default.ParseArguments<Options>(argv)
  match result with
  | :? Parsed<Options> as parsed -> run parsed.Value
  | :? NotParsed<Options> as notParsed -> fail notParsed.Errors
  | _ -> 1
