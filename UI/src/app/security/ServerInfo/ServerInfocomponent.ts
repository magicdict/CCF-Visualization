import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IServerInfo } from '../Model';


@Component({
  selector: 'app-root',
  templateUrl: './ServerInfo.component.html',
})
export class ServerInfoComponent implements OnInit {
  constructor(private route: ActivatedRoute) { }
  ngOnInit(): void {
    this.route.data
      .subscribe((xxx: { data: IServerInfo[] }) => {
        console.log(xxx.data[0].server_ip);
        console.log(new Date(xxx.data[0].start_time));
      });
  }
}
