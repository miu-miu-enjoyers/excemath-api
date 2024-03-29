// excemath API - open source API for educational projects related to mathematics
// Copyright (C) 2023  miu-miu enjoyers
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//
// Contact us:
// i.   By paper mail: 23 Yevhena Patona street, Zaliznychnyi raion, Lviv, Lviv oblast, 79040, Ukraine
// ii.  By email: vladyslav.yakubovskyi.work@gmail.com
//
// The official repository page on GitHub:
// https://github.com/miu-miu-enjoyers/excemath-api

namespace excemathApiUnitTests
{
    public class StudentTests // Tests the Student class.
    {
        #region Fields

        private readonly int[] _enumValues = (int[])Enum.GetValues(typeof(MathProblemTypes));
        private readonly Guid _studentId = Guid.NewGuid();

        private const string _NICKNAME = "Nickname";
        private const string _FIRST_NAME = "First name";
        private const string _LAST_NAME = "Last name";

        private readonly Guid _firstSolvedMathProblemId = Guid.NewGuid();
        private readonly Guid _secondSolvedMathProblemId = Guid.NewGuid();
        private readonly Guid _thirdSolvedMathProblemId = Guid.NewGuid();

        private const int _EXPERIENCE = 500;

        private readonly List<int> _correctAnswersOrder = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();
        private readonly List<int> _incorrectAnswersOrder = Enumerable.Repeat(0, Enum.GetValues(typeof(MathProblemTypes)).Length).ToList();

        private const string _LOCATION = "Location";
        private const string _ABOUT = "About.";

        private enum TestEnum
        {
            Zero = 0,
            One = 1,
            Two = 2,
            Three = 3
        }

        private const string _ZERO = "Zero";
        private const string _ONE = "One";
        private const string _TWO = "Two";
        private const string _THREE = "Three";

        #endregion

        #region Tests

        [SetUp]
        public void SetUp() // Sets up the tests.
        {
            for (int ii = 0; ii < _enumValues.Length; ii++)
            {
                _correctAnswersOrder[ii] = ii;
                _incorrectAnswersOrder[ii] = ii + 1;
            }
        }

        [Test]
        public void EnumAndListToDictionaryTest() // Tests the Enum-List to Dictionary logic.
        {
            int[] keys = (int[])Enum.GetValues(typeof(TestEnum));
            List<string> values = new() { _ZERO, _ONE, _TWO, _THREE };

            Dictionary<TestEnum, string> expectedDictionary = new()
            {
                { TestEnum.Zero, _ZERO },
                { TestEnum.One, _ONE },
                { TestEnum.Two, _TWO },
                { TestEnum.Three, _THREE }
            };

            Dictionary<TestEnum, string> actualDictionary = new();

            for (int ii = 0; ii < keys.Length; ii++)
                actualDictionary.Add((TestEnum)keys[ii], values[ii]);

            actualDictionary.Should().BeEquivalentTo(expectedDictionary);
        }

        [Test]
        public void FromDtoTest() // Tests the Student(StudentDto dto) constructor.
        {
            StudentDto dto = new() // Initial Data Transfer Object.
            {
                Id = _studentId,
                Nickname = _NICKNAME,
                FirstName = _FIRST_NAME,
                LastName = _LAST_NAME,
                SolvedMathProblems = new() { _firstSolvedMathProblemId, _secondSolvedMathProblemId, _thirdSolvedMathProblemId },
                Experience = _EXPERIENCE,
                CorrectAnswersOrder = _correctAnswersOrder,
                IncorrectAnswersOrder = _incorrectAnswersOrder,
                Location = _LOCATION,
                About = _ABOUT
            };

            Student expectedStudent = new() // Expected student.
            {
                Id = _studentId,
                Nickname = _NICKNAME,
                FirstName = _FIRST_NAME,
                LastName = _LAST_NAME,
                SolvedMathProblems = new() { _firstSolvedMathProblemId, _secondSolvedMathProblemId, _thirdSolvedMathProblemId },
                Experience = _EXPERIENCE,
                CorrectAnswers = new(),
                IncorrectAnswers = new(),
                Location = _LOCATION,
                About = _ABOUT
            };

            for (int ii = 0; ii < _enumValues.Length; ii++)
            {
                expectedStudent.CorrectAnswers.Add((MathProblemTypes)_enumValues[ii], _correctAnswersOrder[ii]);
                expectedStudent.IncorrectAnswers.Add((MathProblemTypes)_enumValues[ii], _incorrectAnswersOrder[ii]);
            }

            Student actualStudent = new(dto); // Actual student.

            actualStudent.Should().BeEquivalentTo(expectedStudent);
        }

        #endregion
    }
}
