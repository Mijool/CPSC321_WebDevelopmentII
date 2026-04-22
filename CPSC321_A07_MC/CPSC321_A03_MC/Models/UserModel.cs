using System.ComponentModel.DataAnnotations;

namespace CPSC321_A07_MC.Models;

public class UserModel
{

    public int ID { get; set; } //no regex here because the program sets the ID, not the User


    [Required(ErrorMessage = "Please enter a first name between 3 and 10 letters")]
    [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "Please enter a first name between 3 and 10 letters")]
    public string FirstName { get; set; }


    [Required(ErrorMessage = "Please enter a last name between 3 and 10 letters")]
    [RegularExpression(@"^[a-zA-Z]{3,10}$", ErrorMessage = "Please enter a last name between 3 and 10 letters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Please select a department")]
    public string Department { get; set; }

    [Required(ErrorMessage = "Please select a postition")]
    public string Position { get; set; }

    [Range(0, 30, ErrorMessage = "Please enter a whole number between 0 and 30")]
    [Required(ErrorMessage = "Please enter length of tenure")]
    [RegularExpression(@"^\d{1,2}$", ErrorMessage = "Please enter a whole number between 0 and 30")]
    public int YearsOfExperience { get; set; }


    [Range(1, 100_000, ErrorMessage = "Please enter a number between 1 and 100,000")]
    [Required(ErrorMessage = "Please enter salary")]

    [RegularExpression(@"^\d{1,6}(\.\d{1,2})?$", ErrorMessage = "Please enter a number between 1 and 100,000")]
    public double Salary { get; set; }
    
    [RegularExpression(@"^[A-Za-z0-9 ]*$", ErrorMessage = "Enter a comment")]
    public string? DeletionComment { get; set; } //allow comment to be nullable as it wont be given a value until after it gets deleted
}
/*
 Data annotation: these can be stacked
[Range(low#, high#, ErrorMessage = "error")]
[Required(ErrorMessage = "error")]
[RegularExpression(@"^a-zA-Z+$"), ErrorMessage = ""]
[RegularExpression(@"^\d(\.\d{1,2}?$"), ErrorMessage = ""]
they only affect the first thing below them
 */

/*
 <div class="form-group">
                <label asp-for="Department" class="control-label"></label>
                <select asp-for="Department" class="form-select" id="DepartmentSelection">
                    <option selected disabled value="">Choose...</option>
                    <option value="IT">IT</option>
                    <option value="Business">Business</option>
                </select>
</div>
            <div class="form-group" id="ITPositions" disabled>
                <label asp-for="Position" class="control-label"></label>
                <select asp-for="Position" class="form-control">
                    <option selected disabled value="">Choose...</option>
                    <option>Software Engineer</option>
                    <option>Data Scientist</option>
                    <option>Data Analyst</option>
                    <option>Security Engineer</option>
                </select>
                <span asp-validation-for="Position" class="text-danger"></span>
            </div>
            <div class="form-group" id="BusinessPositions" disabled>
                <label asp-for="Position" class="control-label"></label>
                <select asp-for="Position" class="form-control">
                    <option selected disabled value="">Choose...</option>
                    <option>Sales</option>
                    <option>Accountant</option>
                    <option>Business Analyst</option>
                    <option>Project Manager</option>
                </select>
                <span asp-validation-for="Position" class="text-danger"></span>
            </div>




<script>
    $(document).ready(function () {
        $("#DepartmentSelection").change(function () {
            if ($(this).val() === 'Business') {
                $("BusinessPositions").prop('disabled', false).val("");
                $("ITPositions").prop('disabled', true).val("");
                
            } else if ($(this).val() === 'IT') {
                $("ITPositions").prop('disabled', false).val("");
                $("BusinessPositions").prop('disabled', true).val("");
            }
        })
    })
    
</script>
 */