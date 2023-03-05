using System.Linq;
using Microsoft.ML.Trainers.FastTree;
using Microsoft.ML;

namespace HateSpeechPL
{
    public partial class HateSpeechModel
    {
        /// <summary>
        /// Retrains model using the pipeline generated as part of the training process. For more information on how to load data, see aka.ms/loaddata.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <param name="trainData"></param>
        /// <returns></returns>
        public static ITransformer RetrainPipeline(MLContext mlContext, IDataView trainData)
        {
            var pipeline = BuildPipeline(mlContext);
            var model = pipeline.Fit(trainData);

            return model;
        }

        /// <summary>
        /// build the pipeline that is used from model builder. Use this function to retrain model.
        /// </summary>
        /// <param name="mlContext"></param>
        /// <returns></returns>
        public static IEstimator<ITransformer> BuildPipeline(MLContext mlContext)
        {
            // Data process configuration with pipeline data transformations
            var pipeline = mlContext.Transforms.ReplaceMissingValues(@"sarcasm_irony", @"sarcasm_irony")      
                                    .Append(mlContext.Transforms.Text.FeaturizeText(inputColumnName:@"text",outputColumnName:@"text"))      
                                    .Append(mlContext.Transforms.Concatenate(@"Features", new []{@"sarcasm_irony",@"text"}))      
                                    .Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName:@"negative_emotion",inputColumnName:@"negative_emotion"))      
                                    .Append(mlContext.MulticlassClassification.Trainers.OneVersusAll(binaryEstimator:mlContext.BinaryClassification.Trainers.FastForest(new FastForestBinaryTrainer.Options(){NumberOfTrees=4,NumberOfLeaves=4,FeatureFraction=1F,LabelColumnName=@"negative_emotion",FeatureColumnName=@"Features"}),labelColumnName:@"negative_emotion"))      
                                    .Append(mlContext.Transforms.Conversion.MapKeyToValue(outputColumnName:@"PredictedLabel",inputColumnName:@"PredictedLabel"));

            return pipeline;
        }
    }
}
