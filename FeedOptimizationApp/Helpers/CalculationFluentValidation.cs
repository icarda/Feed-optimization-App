using DataLibrary.Models;
using FluentValidation;

namespace FeedOptimizationApp.Helpers
{
    public class CalculationValidator : AbstractValidator<CalculationEntity>
    {
        public CalculationValidator()
        {
            RuleFor(x => x.Type).NotEmpty().NotNull().WithMessage("Type must be selected.");
            RuleFor(x => x.GrazingId).GreaterThan(0).WithMessage("Grazing must be selected.");
            RuleFor(x => x.BodyWeightId).GreaterThan(0).WithMessage("Body weight must be selected.");
            RuleFor(x => x.DietQualityEstimateId).GreaterThan(0).WithMessage("Diet quality estimate must be selected.");
            RuleFor(x => x.KidsLambsId).GreaterThan(0).WithMessage("Number of suckling kids/lambs must be selected.");
            RuleFor(x => x.SpeciesId).GreaterThan(0).WithMessage("Species must be selected.");
        }
    }
}