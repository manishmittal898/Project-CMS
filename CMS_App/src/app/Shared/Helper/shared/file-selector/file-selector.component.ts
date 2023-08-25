import { ToastrService } from 'ngx-toastr';
import { Component, EventEmitter, Output, Input, Sanitizer } from '@angular/core';
import { AlertService } from 'src/app/Shared/Services/alert.service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';


export class FileInfo {
  private readonly _file: File;
  private readonly _name: string;
  private _fileBase64!: string;
  private readonly _size: string;
  private readonly _sizeSuffix: string;

  constructor(file: File) {
    const SIZES = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
    this._size = '0';
    this._sizeSuffix = SIZES[0];

    let reader = new FileReader();
    reader.onload = e => {
      this._fileBase64 = e.target!.result!.toString();
    };
    reader.readAsDataURL(file);

    this._file = file;
    this._name = file.name;
    this._fileBase64 = this.FileBase64;
    const DECIMALS = 2;
    const KB = 1024;
    const DECIMAL_MARKER = DECIMALS < 0 ? 0 : DECIMALS;

    const i = Math.floor(Math.log(file.size) / Math.log(KB));

    this._size = (file.size / Math.pow(KB, i)).toFixed(DECIMAL_MARKER);
    this._sizeSuffix = SIZES[i];
  }

  get Name(): string {
    return this._name;
  }

  get FileBase64(): string {
    return this._fileBase64;
  }

  get Size(): string {
    return this._size;
  }

  get SizeSuffix(): string {
    return this._sizeSuffix;
  }
}

@Component({
  selector: 'app-file-selector',
  templateUrl: './file-selector.component.html',
  providers: [AlertService]
})
export class FileSelectorComponent {
  private _files: FileInfo[];
  private _allowFiles: string[];

  @Output() readonly FileSelected: EventEmitter<FileInfo>;
  @Output() readonly FilesChanged: EventEmitter<FileInfo[]>;
  @Input() IsShowFiles: boolean = true;
  @Input() MaxFileLength: number = 1;//for all
  @Input() CurrentFileLength: number = 0;//for all
  @Input() CurrentFiles?: FileInfo[];
  @Input() FileFilter: string;
  get Files(): FileInfo[] {
    return this._files;
  }
  constructor(readonly _alertService: AlertService, private readonly toast: ToastrService, public domSanitizer: DomSanitizer) {
    this.FileSelected = new EventEmitter();
    this.FilesChanged = new EventEmitter();
    this._files = [];
    this.FileFilter = "image/*,.doc,.docx,.ppt,.pptx,.pdf,.xlx,.xlsx,.txt";
    this._allowFiles = ['.jpeg', '.gif','.webp', '.png', '.jpg', '.TIFF', '.PSD', '.EPS', '.RAW', '.INDD', '.AI', '.doc', '.docx', '.ppt', '.pptx', '.pdf', '.txt', '.xlx', '.xlsx', '.BMP', '.SVG', '.mkv'];
  }
  RemoveFile(file: FileInfo) {
    let index = this._files.indexOf(file);
    if (index > -1) {
      this._files.splice(index, 1);
      this.CurrentFileLength = this.CurrentFileLength - 1;
      this.FilesChanged.emit(this.Files);
    }

  }

  openFile(file: FileInfo) {
    if (file) {
      let fileUrl = file.FileBase64;
      let isDoc = ['doc', 'docx', 'ppt', 'pptx', 'pdf', 'txt', 'xlx', 'xlsx'].some(x => x.toLowerCase() === file.Name.split('.')[1].toLowerCase())
      var newWin = open("'url'", "_blank");
      if (isDoc) {
        newWin!.document.write(`<iframe title="PDF" src="${fileUrl}"  height="99%" width="100%"></iframe>`);
      }
      else {
        newWin!.document.write(`<img  src="${fileUrl}" style="margin:auto; display:flex;"/>`);
      }
    }
  }

  HandleFileInput(event: any) {
    const TotalFilesCount = (this.CurrentFileLength ? this.CurrentFileLength : this.Files?.length) + 1
    if (TotalFilesCount <= this.MaxFileLength) {
      let files = event.target.files;
      if (files.length == 0) {
        this.FileSelected.emit(undefined);
        return;
      }
      for (let index = 0; index < files.length; index++) {
        let file = files.item(index);
        let extIndex = file!.name.lastIndexOf('.');
        let ext = file!.name.substring(extIndex);
        let isAllowed = this._allowFiles.some(x => x.toLowerCase() === ext.toLowerCase());
        if (!isAllowed) {
          this.toast.warning('Selected  file  format not allowed to upload', 'File Upload');
          return;
        }
        if (file == null || file.size == 0) {
          continue;
        }

        let fileInfo = new FileInfo(file);
        // if (this.maxFile == 0 || this.Files.length <= this.maxFile) {
        this._files.push(fileInfo);
        // }
        this.FileSelected.emit(fileInfo);

      }
      setTimeout(() => {
        let f = this.Files;
        this._files = this.Files.filter(function (elem, index, self) {
          return index == self.findIndex(x => x.FileBase64 == elem.FileBase64);
        })

        this.FilesChanged.emit(this.Files);
      }, 150);
    }
    else {
      this.toast.warning('Maximum File upload limit exceed', 'File Upload');
      return;
    }
  }
  getFileType(fileName: string) {
    const ext = fileName?.split('.')[fileName?.split('.').length - 1]?.toLowerCase()??'';
    if (['doc', 'docx', 'ppt', 'pptx', 'pdf', 'txt', 'xlx', 'xlsx'].some(x => x.toLowerCase() === ext)) {
      return 'doc';
    } else if (['jpeg', 'gif', 'png', 'jpg', 'svg','webp'].some(x => x.toLowerCase() === ext)) {
      return 'image';
    }
    else if (['mp4', 'mkv', 'avi',].some(x => x.toLowerCase() === ext)) {
      return 'video';
    } else {
      return ext;
    }
  }
}
