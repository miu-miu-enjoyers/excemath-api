﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace excemathApi.Models;

/// <summary>
/// Represents a <see cref="MathProblem"/> class object as a Data Transfer Object.
/// </summary>
[Table("MathProblems")]
public class MathProblemDto
{
    /// <inheritdoc cref="MathProblem.Id"/>
    /// <remarks>Required.</remarks>
    [Key]
    public required Guid Id { get; set; }

    /// <inheritdoc cref="MathProblem.Type"/>
    /// <remarks>Required.</remarks>
    public required MathProblemTypes Type { get; set; }

    /// <inheritdoc cref="MathProblem.Difficulty"/>
    /// <remarks>Required.</remarks>
    public required int Difficulty { get; set; }

    /// <summary>
    /// A normal text part in the math problem question.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required string QuestionNormalText { get; set; }

#nullable enable 

    /// <summary>
    /// A LaTeX part in the math problem question.
    /// </summary>
    public string? QuestionLatex { get; set; }

#nullable restore

    /// <summary>
    /// The order of the <see cref="MathOption.RenderAsLatex"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<bool> OptionsRenderAsLatexOrder { get; set; }

    /// <summary>
    /// The order of the <see cref="MathOption.Index"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<int> OptionsIndexOrder { get; set; }

    /// <summary>
    /// The order of the <see cref="MathOption.Content"/> property values of the options.
    /// </summary>
    /// <remarks>Required.</remarks>
    public required List<string> OptionsContentOrder { get; set; }

    /// <inheritdoc cref="MathProblem.Answer"/>
    public required int Answer { get; set; }

#nullable enable

    /// <summary>
    /// The order of the <see cref="MathExposition.NormalText"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionNormalTextsOrder { get; set; }

    /// <summary>
    /// The order of the <see cref="MathExposition.Latex"/> property values of the step-by-step solution.
    /// </summary>
    public List<string?>? SolutionLatexOrder { get; set; }
}
