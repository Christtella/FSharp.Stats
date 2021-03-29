﻿namespace FSharp.Stats


module Normalization =

    /// z normalization using the population standard deviation of population
    //Bortz J., Schuster C., Statistik für Human- und Sozialwissenschaftler, 7 (2010), p. 35
    let zScoreTransformPopulation (yVal:Vector<float>) =
        let yMean = Seq.mean yVal 
        let std   = Seq.stDevPopulation yVal
        yVal |> Vector.map (fun x -> (x - yMean) / std) 

    /// z normalization using the sample standard deviation
    //Bortz J., Schuster C., Statistik für Human- und Sozialwissenschaftler, 7 (2010), p. 35
    let zScoreTrans (yVal:Vector<float>) =
        let yMean = Seq.mean yVal
        let std   = Seq.stDev yVal
        yVal |> Vector.map (fun x -> (x - yMean) / std) 

    /// As used by Deseq2, see: https://github.com/hbctraining/DGE_workshop/blob/master/lessons/02_DGE_count_normalization.md 
    ///
    /// Rows are genes, columns are samples
    let medianOfRatios (data:Matrix<float>) =
        let sampleWiseCorrectionFactors =
            data
            |> Matrix.mapiRows (fun _ v -> 
                let geometricMean = Seq.meanGeometric v           
                Seq.map (fun s -> s / geometricMean) v
                ) 
            |> matrix
            |> Matrix.mapiCols (fun _ v -> Vector.median v)
            |> vector
        data
        |> Matrix.mapi (fun r c v ->
            v / sampleWiseCorrectionFactors.[c]
        )