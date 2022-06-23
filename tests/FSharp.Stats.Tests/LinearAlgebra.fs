module LinearAlgebraTests

open Expecto

open FSharp.Stats
open FSharp.Stats.Algebra
open TestExtensions

[<Tests>]
let managedSVDTests =

    let svdManaged A = 
        let s,u,vt  = LinearAlgebraManaged.SVD A
        let sM = 
            let tmp= Matrix.create A.NumRows A.NumCols 0. 
            for i = 0 to s.Length-1 do 
                tmp.[i,i] <- s.[i]
            tmp
        u,sM,vt
    
    let mSmallerN = Matrix.ofJaggedArray [| [|2.;-1.;2.;-1.|];  [|4.;3.;4.;3.|]; [|9.;13.;-13.;9.|]; |]
    let mEqualN = Matrix.ofJaggedArray [| [|2.;-1.|]; [|9.;13.|]; |]

    testList "LinearAlgebra.LinearAlgebraManaged.SVD" [
        testCase "m=n Matrix: Recover from decomposition" <| fun () -> 
            let u,s,vt = svdManaged mEqualN
            let mEqualNRecov = (u * s * vt)
            let m = mEqualN |> Matrix.toJaggedArray |> Array.concat
            let m' = mEqualNRecov |> Matrix.toJaggedArray |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high m m' "Matrices computed by SVD did not yield the initial matrix when multiplied."
        
        testCase "m=n Matrix: u and vt consist of unit vectors, row- and column- wise." <| fun () -> 
            let u,s,vt = svdManaged mEqualN
            let vecNorms = 
                [
                u |> Matrix.mapCols Vector.norm |> RowVector.toArray
                vt|> Matrix.mapCols Vector.norm |> RowVector.toArray
                u |> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                vt|> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                ]
                |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high (Array.create vecNorms.Length 1.) vecNorms "Matrices computed by SVD did not consist of unit vectors, row- and column- wise."
        
        testCase "m=n Matrix: s contains correct singular values." <| fun () -> 
            let s,u,vt = LinearAlgebraManaged.SVD  mEqualN
            TestExtensions.sequenceEqual Accuracy.high ([|15.81461344;2.213142934|]) s "Matrices computed by SVD did not yield correct singular values."
        
        testCase "m<n Matrix: Recover from decomposition" <| fun () -> 
            let u,s,vt = svdManaged mSmallerN
            let mSmallernRecov = (u * s * vt)
            let m = mSmallerN |> Matrix.toJaggedArray |> Array.concat
            let m' = mSmallernRecov |> Matrix.toJaggedArray |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high m m' "Matrices computed by SVD did not yield the initial matrix when multiplied."
        
        testCase "m<n Matrix: u and vt consist of unit vectors, row- and column- wise." <| fun () -> 
            let u,s,vt = svdManaged mSmallerN
            let vecNorms = 
                [
                u |> Matrix.mapCols Vector.norm |> RowVector.toArray
                vt|> Matrix.mapCols Vector.norm |> RowVector.toArray
                u |> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                vt|> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                ]
                |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high (Array.create vecNorms.Length 1.) vecNorms "Matrices computed by SVD did not consist of unit vectors, row- and column- wise."
        
        testCase "m<n Matrix: s contains correct singular values." <| fun () -> 
            let s,u,vt = LinearAlgebraManaged.SVD  mSmallerN
            TestExtensions.sequenceEqual Accuracy.high ([|22.51999394;6.986424855;2.00991059|]) s "Matrices computed by SVD did not yield correct singular values."
            
        testCase "m>n Matrix: Recover from decomposition" <| fun () -> 
            let u,s,vt = svdManaged mSmallerN.Transpose
            let mSmallernRecov = (u * s * vt)
            let m = mSmallerN.Transpose |> Matrix.toJaggedArray |> Array.concat
            let m' = mSmallernRecov |> Matrix.toJaggedArray |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high m m' "Matrices computed by SVD did not yield the initial matrix when multiplied."
    
        testCase "m>n Matrix: u and vt consist of unit vectors, row- and column- wise." <| fun () -> 
            let u,s,vt = svdManaged mSmallerN.Transpose
            let vecNorms = 
                [
                u |> Matrix.mapCols Vector.norm |> RowVector.toArray
                vt|> Matrix.mapCols Vector.norm |> RowVector.toArray
                u |> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                vt|> Matrix.mapRows (fun x -> x.Transpose |> Vector.norm) |> Vector.toArray
                ]
                |> Array.concat
            TestExtensions.sequenceEqual Accuracy.high (Array.create vecNorms.Length 1.) vecNorms "Matrices computed by SVD did not consist of unit vectors, row- and column- wise."
        
        testCase "m>n Matrix: s contains correct singular values." <| fun () -> 
            let s,u,vt = LinearAlgebraManaged.SVD  mSmallerN.Transpose
            TestExtensions.sequenceEqual Accuracy.high ([|22.51999394;6.986424855;2.00991059|]) s "Matrices computed by SVD did not yield correct singular values."
    ]
    

[<Tests>]
let nullspace =
  
    let mSmallerN = Matrix.ofJaggedArray [| [|2.;-1.;2.;-1.|];  [|4.;3.;4.;3.|]; [|9.;13.;-13.;9.|]; |]
    
    testList "LinearAlgebra.nullspace" [
        testCase "accuracy 1e-5" <| fun () -> 
            let ns = LinearAlgebra.nullspace (Accuracy=1e-5) mSmallerN
            let prod = 
                mSmallerN * ns
                |> Matrix.toJaggedSeq
                |> Seq.concat
            let expected = seq {0.;0.;0.;}
            TestExtensions.sequenceEqual Accuracy.veryHigh expected prod  "A * (nullspace A) should be matrix of zeros"
    ]
    
