module MLTests
open Expecto
    
open FSharp.Stats
open FSharp.Stats.ML
open TestExtensions

module SimilarityMetrics = 
    
    type private testType = {
        testVal1: float
        testVal2: string []
    }
    let private stringTestSetX = set ["A"; "B"; "C"]
    let private stringTestSetY = set ["C";"D"]
    let private floatTestSetX = set [1.;2.;3.]
    let private floatTestSetY = set [2.;5.]
    let private recordTypeSetX = 
        set [
                {testVal1=1.;testVal2=[|"A"|]}
                {testVal1=2.;testVal2=[|"B"|]}
                {testVal1=3.;testVal2=[|"C"|]}
            ]
    let private recordTypeSetY = 
        set [
                {testVal1=2.;testVal2=[|"B"|]}
                {testVal1=5.;testVal2=[|"D"|]}
            ]
    let private emptyfloatSet : Set<float>          = Set.empty
    let private emptystringSet : Set<string>        = Set.empty
    let private emptyRecordTypeSet : Set<testType>  = Set.empty

    [<Tests>]
    let jaccardIndexTests =
        testList "SimilarityMetrics.jaccard" [

            testCase "Empty float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Jaccard index for two empty float sets was not 1."

            testCase "Empty String Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard emptystringSet emptystringSet)
                Expect.floatClose Accuracy.high actual 1. "Jaccard index for two empty float sets was not 1."

            testCase "Empty Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard emptyRecordTypeSet emptyRecordTypeSet)
                Expect.floatClose Accuracy.high actual 1. "Jaccard index for two empty float sets was not 1."

            testCase "Equal Sets"<| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard floatTestSetX floatTestSetX)
                Expect.floatClose Accuracy.veryHigh actual 1. "Jaccard index for two equal sets was not 1."

            testCase "float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard floatTestSetX floatTestSetY)
                Expect.floatClose Accuracy.high actual (1./4.) "Jaccard index for two float sets not correct"

            testCase "string Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard stringTestSetX stringTestSetY)
                Expect.floatClose Accuracy.high actual (1./4.) "Jaccard index for two string sets not correct"

            testCase "Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.jaccard recordTypeSetX recordTypeSetY)
                Expect.floatClose Accuracy.high actual (1./4.) "Jaccard index for two recordType sets not correct"
        ]

    [<Tests>]
    let overlapIndexTests =
        testList "SimilarityMetrics.overlap" [

            testCase "Empty String Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap emptystringSet emptystringSet)
                Expect.floatClose Accuracy.high actual 1. "Overlap index for two empty float sets was not 1."

            testCase "Empty Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap emptyRecordTypeSet emptyRecordTypeSet)
                Expect.floatClose Accuracy.high actual 1. "Overlap index for two empty float sets was not 1."

            testCase "Overlap of nonEmpty with empty set" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap emptyRecordTypeSet recordTypeSetX)
                Expect.floatClose Accuracy.high actual 0. "Overlap index for two empty float sets was not 1."

            testCase "Equal Sets"<| fun () ->
                let actual = (SimilarityMetrics.Set.overlap floatTestSetX floatTestSetX)
                Expect.floatClose Accuracy.veryHigh actual 1. "Jaccard index for two equal sets was not 1."

            testCase "float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap floatTestSetX floatTestSetY)
                Expect.floatClose Accuracy.high actual (1./2.) "Overlap index for two float sets not correct"

            testCase "string Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap stringTestSetX stringTestSetY)
                Expect.floatClose Accuracy.high actual (1./2.) "Overlap index for two string sets not correct"

            testCase "record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap recordTypeSetX recordTypeSetY)
                Expect.floatClose Accuracy.high actual (1./2.) "Overlap index for two recordType sets not correct"

            testCase "Empty float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.overlap emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Overlap index for two empty float sets was not 1."
        ]

    [<Tests>]
    let sorensenDiceIndexTests =
        testList "SimilarityMetrics.sorensenDice" [

            testCase "Empty float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Sorensen Dice index for two empty float sets was not 1."

            testCase "Empty String Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice emptystringSet emptystringSet)
                Expect.floatClose Accuracy.high actual 1. "Sorensen Dice index for two empty float sets was not 1."

            testCase "Empty Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice emptyRecordTypeSet emptyRecordTypeSet)
                Expect.floatClose Accuracy.high actual 1. "Sorensen Dice index for two empty float sets was not 1."

            testCase "Equal Sets"<| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice floatTestSetX floatTestSetX)
                Expect.floatClose Accuracy.veryHigh actual 1. "Sorensen Dice index for two equal sets was not 1."

            testCase "float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice floatTestSetX floatTestSetY)
                Expect.floatClose Accuracy.high actual (2./5.) "Sorensen Dice index for two float sets not correct"

            testCase "string Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice stringTestSetX stringTestSetY)
                Expect.floatClose Accuracy.high actual (2./5.) "Sorensen Dice index for two string sets not correct"

            testCase "record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice recordTypeSetX recordTypeSetY)
                Expect.floatClose Accuracy.high actual (2./5.) "Sorensen Dice index for two recordType sets not correct"

            testCase "Overlap of nonEmpty with empty set" <| fun () ->
                let actual = (SimilarityMetrics.Set.sorensenDice emptyRecordTypeSet recordTypeSetX)
                Expect.floatClose Accuracy.high actual 0. "Sorensen Dice index for two empty float sets was not 1."
        ]

    [<Tests>]
    let tverskyIndexTests =
        testList "SimilarityMetrics.tversky" [
            testCase "Zero Weights" <| fun () ->
                let actual = (SimilarityMetrics.Set.tversky 0. 0. emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Tversky index with zero weights was not 1."

            testCase "Empty float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tversky 1. 1. emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Tversky index for two empty float sets was not 1."

            testCase "Empty String Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tversky 1. 1. emptystringSet emptystringSet)
                Expect.floatClose Accuracy.high actual 1. "Tversky index for two empty float sets was not 1."

            testCase "Empty Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tversky 1. 1. emptyRecordTypeSet emptyRecordTypeSet)
                Expect.floatClose Accuracy.high actual 1. "Tversky index for two empty float sets was not 1."

            testCase "prototypeWeight = variantWeight = 0.5 equals SorensenDice" <| fun () ->
                let actual      = (SimilarityMetrics.Set.tversky 0.5 0.5 floatTestSetX floatTestSetX)
                let expected    = (SimilarityMetrics.Set.sorensenDice floatTestSetX floatTestSetX)
                Expect.floatClose Accuracy.high actual expected "Tversky with 0.5 weights was not equal to sorensen dice"

            testCase "prototypeWeight = variantWeight = 1. equals jaccard" <| fun () ->
                let actual      = (SimilarityMetrics.Set.tversky 0.5 0.5 floatTestSetX floatTestSetX)
                let expected    = (SimilarityMetrics.Set.jaccard floatTestSetX floatTestSetX)
                Expect.floatClose Accuracy.high actual expected "Tversky with 1. weights was not equal to jaccard"
        ]

    [<Tests>]
    let tverskySymmetricIndexTests =
        testList "SimilarityMetrics.Symmetric Tversky" [
            testCase "Zero Weights" <| fun () ->
                let actual = (SimilarityMetrics.Set.tverskySymmetric 0. 0. emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Symmetric Tversky index with zero weights was not 1."

            testCase "Empty float Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tverskySymmetric 1. 1. emptyfloatSet emptyfloatSet)
                Expect.floatClose Accuracy.high actual 1. "Symmetric Tversky index for two empty float sets was not 1."

            testCase "Empty String Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tverskySymmetric 1. 1. emptystringSet emptystringSet)
                Expect.floatClose Accuracy.high actual 1. "Symmetric Tversky index for two empty float sets was not 1."

            testCase "Empty Record Type Set Similarity" <| fun () ->
                let actual = (SimilarityMetrics.Set.tverskySymmetric 1. 1. emptyRecordTypeSet emptyRecordTypeSet)
                Expect.floatClose Accuracy.high actual 1. "Symmetric Tversky index for two empty float sets was not 1."

        ]

module PCA =         
    [<Tests>]
    let pcaTests =
        //The Implementation was compared to the R function prcomp(). The implementation is based on remarks found in https://stats.stackexchange.com/a/134283
        //Signs of loadings and principal components (scores) can differ from the R implementation due to different svd implementations being used internally.
        //Colab workbook for direct comparison to prcomps output is accessible at: https://colab.research.google.com/drive/1DJ4ky5F5kBM87JprmAbx_gTHqSdz3vqU?usp=sharing
    
        let data = 
            [
                [1.0; 2.0;1.0; 2.0;];
                [1.1; 2.1;1.1; 2.1;];
                [-1.0; -2.0;1.0; 2.0;];
                [-1.1; -2.1;1.1; 2.1;];
                [-1.15; -2.15;1.15; 2.15;];
            ]
            |> FSharp.Stats.Matrix.ofJaggedList
        
        let dataNan = 
            [
                [nan; 2.0;1.0; 2.0;];
                [1.1; 2.1;1.1; 2.1;];
                [-1.0; -2.0;1.0; 2.0;];
                [-1.1; -2.1;1.1; 2.1;];
                [-1.15; -2.15;1.15; 2.15;];
            ]
            |> FSharp.Stats.Matrix.ofJaggedList
        
        let dataInf = 
            [
                [infinity; 2.0;1.0; 2.0;];
                [1.1; 2.1;1.1; 2.1;];
                [-1.0; -2.0;1.0; 2.0;];
                [-1.1; -2.1;1.1; 2.1;];
                [-1.15; -2.15;1.15; 2.15;];
            ]
            |> FSharp.Stats.Matrix.ofJaggedList
        
        let dataNegInf = 
            [
                [-infinity; 2.0;1.0; 2.0;];
                [1.1; 2.1;1.1; 2.1;];
                [-1.0; -2.0;1.0; 2.0;];
                [-1.1; -2.1;1.1; 2.1;];
                [-1.15; -2.15;1.15; 2.15;];
            ]
            |> FSharp.Stats.Matrix.ofJaggedList

        testList "PCA" [

            testCase "center_catch_nan" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.center dataNan |> ignore) "did not catch nan in input."

            testCase "center_catch_inf" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.center dataInf |> ignore) "did not catch inf in input."
            
            testCase "center_catch_negInf" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.center dataNegInf |> ignore) "did not catch -inf in input."

            testCase "compute_catch_nan" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.compute dataNan |> ignore) "did not catch nan in input."

            testCase "compute_catch_inf" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.compute dataInf |> ignore) "did not catch inf in input."

            testCase "compute_catch_negInf" <| fun () ->
                Expect.throws (fun _ -> ML.Unsupervised.PCA.compute dataNegInf |> ignore) "did not catch -inf in input."

            testCase "centerMatrixColumnWise" <| fun () ->
                let m = ML.Unsupervised.PCA.center data
                let correctCentered = 
                    [|[|1.1751115628; 1.1995649372; -1.1666666667; -1.1666666667|];
                    [|1.2706490883; 1.2489297494; 0.5000000000; 0.5000000000|];
                    [|-0.7356389458; -0.7750275520; -1.1666666667; -1.1666666667|];
                    [|-0.8311764713; -0.8243923643; 0.5000000000; 0.5000000000|];
                    [|-0.8789452340; -0.8490747704; 1.3333333333; 1.3333333333|]|]
                    |> matrix
                TestExtensions.sequenceEqual Accuracy.low m correctCentered "matrix was centered incorrectly."

            testCase "compute_VarianceOfComponent" <| fun () ->
                let c = ML.Unsupervised.PCA.center data
                let pca = ML.Unsupervised.PCA.compute c
                let correct = vector [|1.794382894; 1.334096203; 0.01942548992; 8.52122906e-17|]
                TestExtensions.sequenceEqual Accuracy.low pca.VarianceOfComponent correct "Variances of components were not calculated correctly."

            testCase "compute_VarExplainedByComponentIndividual" <| fun () ->
                let c = ML.Unsupervised.PCA.center data
                let pca = ML.Unsupervised.PCA.compute c
                let correct = vector [|0.6439619942; 0.3559625358; 7.546993175e-05; 9.770164109e-34|]
                TestExtensions.sequenceEqual Accuracy.low pca.VarExplainedByComponentIndividual correct "Variance explained by individual components was not calculated correctly."

            testCase "compute_VarExplainedByComponentCumulative" <| fun () ->
                let c = ML.Unsupervised.PCA.center data
                let pca = ML.Unsupervised.PCA.compute c
                let correct = vector [|0.6439619942; 0.9999245301; 1.0; 1.0|]
                TestExtensions.sequenceEqual Accuracy.low pca.VarExplainedByComponentCumulative correct "Cumulative variances were not calculated correctly."
            
            testCase "compute_PrincipalComponents" <| fun () ->
                let c = ML.Unsupervised.PCA.center data
                let pca = ML.Unsupervised.PCA.compute c
                let correct = 
                    [|[|2.3539452686; -0.0211916481; -0.0236227100; -0.0; 0.0|];
                      [|0.7595272938; -1.7598322707; 0.0219839347; -0.0; 0.0|];
                      [|0.4117370587; 1.9220381325; 0.0172220468; -0.0; 0.0|];
                      [|-1.3277364367; 0.3280019411; -0.0027888992; -0.0; 0.0|];
                      [|-2.1974731844; -0.4690161547; -0.0127943723; -0.0; 0.0|]|]
                    |> matrix
                TestExtensions.sequenceEqual Accuracy.low pca.PrincipalComponents correct "Principal component scores were not calculated correctly."
            
            testCase "compute_Loadings" <| fun () ->
                let c = ML.Unsupervised.PCA.center data
                let pca = ML.Unsupervised.PCA.compute c
                let correct = 
                    [|[|0.5018458447; -0.4965896061; 0.7082016036; -0.0|];
                      [|0.4979792094; -0.5035828626; -0.7059893820; 0.|];
                      [|-0.5000837205; -0.4999015221; 0.003839354807; -0.7071067812|];
                      [|-0.5000837205; -0.4999015221; 0.003839354807; 0.7071067812|]|]
                    |> matrix
                TestExtensions.sequenceEqual Accuracy.low pca.Loadings correct "Loadings were not calculated correctly."
        ]
