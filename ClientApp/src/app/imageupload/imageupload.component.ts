import { Component, ChangeDetectorRef } from '@angular/core';
import { FormBuilder } from '@angular/forms';
@Component({
  selector: 'app-imageupload',
  templateUrl: './imageupload.component.html'
})
export class ImageuploadComponent {

  imageUrl: any = "https://marnpossastorage.blob.core.windows.net/marnpos-v2-images/default-product.PNG";
  editFile: boolean = true;
  removeUpload: boolean = false;
  currentFile: File = null;
  onFileChange(event) {
    let reader = new FileReader(); // HTML5 FileReader API
    let file = event.target.files[0];
    if (event.target.files && event.target.files[0]) {
      reader.readAsDataURL(file);

      // When file uploads set it to file formcontrol
      reader.onload = () => {
        this.imageUrl = reader.result;
        this.registrationForm.patchValue({
          file: reader.result
        });
        this.editFile = false;
        this.removeUpload = true;
      }
      // ChangeDetectorRef since file is loading outside the zone
      this.cd.markForCheck();        
    }
  }

  constructor(
    public fb: FormBuilder,
    private cd: ChangeDetectorRef

  ) { }
  registrationForm = this.fb.group({
    file: [null]
  })
}
