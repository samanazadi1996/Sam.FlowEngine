import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';
// import { HotToastService } from '@ngneat/hot-toast';
import { StateInterface } from './interfaces/state-interface';

@Injectable({
  providedIn: 'root',
})
export class GeneralService {
  state: StateInterface = {
    profileDisplay: true,
    settingsDisplay: false,
    homeDisplay: false,
    title: ""
  };
  constructor(
    // public toast: HotToastService, 
    private titleService: Title) { }

setButtonsState(state: StateInterface) {
  setTimeout(() => {
    this.state = { ...state };
    this.titleService.setTitle(state.title);
  });
}

  setTitle(title: string) {
    this.state.title = title
    this.titleService.setTitle(title);
  }

  isSuccess(response: any): boolean {
    if (response.errors) {
      var apiErrors: Array<String> = [];
      for (let index = 0; index < response.errors.length; index++) {
        apiErrors.push(response.errors[index].description);
      }
      // this.toast.error(apiErrors.join('<br>'));
      console.log(apiErrors.join('<br>'));
    }
    return response.success;
  }


  getErrors(response: any): Array<String> {
    var apiErrors: Array<String> = [];

    if (response.errors) {
      for (let index = 0; index < response.errors.length; index++) {
        apiErrors.push(response.errors[index].description);
      }
    }

    return apiErrors;
  }
  queryParams(params: any): string {
    const queryParams = new URLSearchParams();

    Object.keys(params).forEach((key) => {
      if (
        params[key] != null &&
        params[key] != '' &&
        params[key] != undefined
      ) {
        queryParams.append(key, params[key]);
      }
    });

    return queryParams.toString();
  }
}
