import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';



@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  gameId: number = 1;
  boardGame: any;
  yearList: number[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadGame();
    console.log(this.boardGame, "ngOnInit");
  }

  loadGame() {
    this.http.get(`/api/BoardGames/${this.gameId}`)
      .subscribe(
        data => {
          console.log(data, "loadGame");
          this.boardGame = data;
        }
      )
  }
}
