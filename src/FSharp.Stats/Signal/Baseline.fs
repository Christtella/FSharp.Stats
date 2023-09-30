namespace FSharp.Stats.Signal

open FSharp.Stats



module Baseline =

    /// <summary>Creates a diagonal 2-D array with the given order and set all diagonal values to x</summary>
    /// <remarks></remarks>
    /// <param name="order"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// </code>
    /// </example>
    let diag order x =
        let arr = Array2D.zeroCreate order order
        for i=0 to (order-1) do 
            arr.[i,i] <- x
        arr

    /// Calculate the n-th order discrete difference along given axis.
    // The first order difference is given by out[n] = a[n+1] - a[n] along the given axis, higher order differences are calculated by using diff recursively.
    let rec diff n (a: float [,]) = //(a : 'T [,] when ^T : (static member (-) : ^T * ^T -> ^T)) =
        if n < 1 then
            a
        else
            let rowCount = Array2D.length1 a
            let colCount = Array2D.length2 a
            let arr = 
                Array2D.init (rowCount) (colCount-1) 
                    (fun rowI colI -> a.[rowI,colI+1] - a.[rowI,colI] )
        
            diff (n-1) arr
    
    // default lamda = 6.   ->  2nd derivative constraint 
    // p = 0.05   ->  Weighting of positive residuals 
    // maxiter = 10 or 20
    
    /// <summary>Asymmetric Least Squares Smoothing by P. Eilers and H. Boelens in 2005</summary>
    /// <remarks></remarks>
    /// <param name="maxiter"></param>
    /// <param name="lambda"></param>
    /// <param name="p"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    /// <example>
    /// <code>
    /// </code>
    /// </example>
    let baselineAls (maxiter:int) (lambda:int) (p:float) (data:float[]) =
        let dMatrix' = 
            let dMatrix = Matrix.ofArray2D (diff 2 (diag data.Length 1.))                            
            (pown 10. lambda) * (dMatrix * dMatrix.Transpose)

        let wInitial = Vector.create (data.Length) 1.
        let y =  Vector.ofArray data        
    

        let rec loop niter (w:Vector<float>) =
            if niter < 1 then                                
                let wMatrix = Matrix.diag w 
                let zMatrix = wMatrix + dMatrix'
                
                Algebra.LinearAlgebra.SolveLinearSystem zMatrix (w .* y)
                //zMatrix.Solve(w .* y)
            else
                let wMatrix = Matrix.diag w 
                let zMatrix = wMatrix + dMatrix'
                //let z = zMatrix.Solve(w .* y)
                let z = Algebra.LinearAlgebra.SolveLinearSystem zMatrix (w .* y)
                // w = p * (y > z) + (1-p) * (y < z)
                let w' = Vector.map2 (fun yi zi -> if yi > zi then p else (1.-p)) y z
            
                loop (niter-1) w'

        loop maxiter wInitial

    /// Asymmetric Least Squares Smoothing using sparse Matrix
    //  by P. Eilers and H. Boelens in 2005
    let baselineAls' (maxiter:int) (lambda:int) (p:float) (data:float[]) =
        let dMatrix' = 
            let dMatrix =
                (diff 2 (diag data.Length 1.))
                |> Matrix.sparseOfArray2D
            (10.**float lambda) * (dMatrix * dMatrix.Transpose)

        let wInitial = Vector.create (data.Length) 1.
        let y =  Vector.ofArray data 


        let rec loop niter (w:Vector<float>) =
            if niter < 1 then
                let wMatrix = Matrix.toSparse (Matrix.diag w)
                let zMatrix = wMatrix + dMatrix'
        
                FSharp.Stats.Algebra.LinearAlgebraManaged.leastSquares zMatrix (w .* y)
            else
                let wMatrix = Matrix.toSparse (Matrix.diag w)
                let zMatrix = wMatrix + dMatrix'
                let z = FSharp.Stats.Algebra.LinearAlgebraManaged.leastSquares zMatrix (w .* y)
                // w = p * (y > z) + (1-p) * (y < z)
                let w' = Vector.map2 (fun yi zi -> if yi > zi then p else (1.-p)) y z
        
                loop (niter-1) w'

        loop maxiter wInitial



