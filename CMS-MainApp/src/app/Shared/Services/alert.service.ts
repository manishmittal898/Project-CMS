import { Injectable } from '@angular/core';
import Swal from 'sweetalert2';



@Injectable({
    providedIn: 'root'
})
export class AlertService {

    constructor() { }

    Error(message: string, title: string = 'Error', okText: string = 'Error'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "error",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    Success(message: string, title: string = 'Success', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "success",
                title: title,
                html: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    Warning(message: string, title: string = 'Warning', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "error",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    Info(message: string, title: string = 'Information', okText: string = 'Ok'): Promise<void> {
        return new Promise<void>((resolve) => {
            Swal.fire({
                icon: "info",
                title: title,
                text: message,
                confirmButtonText: okText,
            }).then(() => resolve());
        });
    }

    Question(message: string, title: string = 'Question', okText: string = 'Yes', cancelText: string = 'No'): Promise<boolean> {
        return new Promise<boolean>((resolve) => {
            Swal.fire({
                icon: "question",
                title: title,
                text: message,
                showCancelButton: true,
                showCloseButton: false,
                confirmButtonText: okText,
                cancelButtonText: cancelText
            }).then((result: any) => {
                if (result.value)
                    return resolve(true);
                else if (result.dismiss == Swal.DismissReason.cancel)
                    return resolve(false);
                else
                    return resolve(false);
            });
        });
    }

}
